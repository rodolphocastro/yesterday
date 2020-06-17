using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api
{
    /// <summary>
    /// Extensions for adding and setting up OpenApi Spec generation and presentation.
    /// </summary>
    public static class OpenApiExtensions
    {
        private static string CurrentVersion => typeof(Startup).Assembly.GetName().Version.ToString(3);
        
        private static OpenApiInfo OpenApiInfo { get; } = new OpenApiInfo
            {
                Title = "Yesterday API",
                Version = CurrentVersion,
                Description = "REST API for Yesterday",
                Contact = new OpenApiContact { Name = "Rodolpho Alves", Url = new Uri("https://github.com/rodolphocastro") }
            };

        /// <summary>
        /// Adds services to Generate the OpenApi json.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOpenApiGen(this IServiceCollection services)
        {
            // TODO: After we add Authentication and Authorization we'll need to review this
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", OpenApiInfo);
            });

            return services;
        }

        /// <summary>
        /// Adds middleware to serve the OpenApi json and Swagger UI.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serveUi"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, bool serveUi = true)
        {
            // TODO: After we add Authentication and Authorization we'll need to review this
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseSwagger();
            _ = serveUi ? 
                app.UseSwaggerUI(s =>
                    {
                        s.RoutePrefix = string.Empty;
                        s.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
                    }) 
                : app;

            return app;
        }

    }
}
