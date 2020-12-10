using Microsoft.AspNetCore.Builder;

namespace FileManager.Web.Middlewares
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerAsync>();

            return app;
        }

        public static IApplicationBuilder EnableSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileManger V1");
            });

            return app;
        }
    }
}