using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DesafioEdCRUD.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }       
            catch(Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                _logger.LogError(ex.Message + ex.InnerException.Message);
                await HandleExceptionAsync(httpContext, "Este registro não pode ser deletado pois está relacionado a um ou mais livros.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(httpContext, ex.Message);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, string exceptionMessage)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exceptionMessage
            }.ToString());
        }
    }
}
