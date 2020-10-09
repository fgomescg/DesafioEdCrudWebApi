using DesafioEdCRUD.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Builder;

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
