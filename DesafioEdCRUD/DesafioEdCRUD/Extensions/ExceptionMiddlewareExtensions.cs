using Contracts;
using DesafioEdCRUD.CustomExceptionMiddleware;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace DesafioEdCRUD.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {    
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
