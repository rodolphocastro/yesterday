using System;

using Api.Auth;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.OpenApi
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
        public static IServiceCollection AddOpenApiGen(this IServiceCollection services, Action<TokenSettings> tknSetup = null)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (tknSetup is null)
            {
                tknSetup = (_) => { };
            }

            var tokenSettings = new TokenSettings();
            tknSetup(tokenSettings);

            return services.AddOpenApiGen(tokenSettings);
        }

        private static IServiceCollection AddOpenApiGen(this IServiceCollection services, TokenSettings settings)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", OpenApiInfo);
                cfg.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            TokenUrl = settings.TokenUrl,
                            AuthorizationUrl = new Uri(settings.AuthorizeUrl, $"?audience={settings.Audience}"),
                            Scopes = Scopes.AllScopes
                        }
                    }
                });
                cfg.OperationFilter<SecurityRequirementOperationFilter>();
            });
            return services;
        }

        /// <summary>
        /// Adds middleware to serve the OpenApi json and Swagger UI.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serveUi"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, bool serveUi = true, Action<TokenSettings> tknSetup = null)
        {
            // TODO: After we add Authentication and Authorization we'll need to review this
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (tknSetup is null)
            {
                tknSetup = (_) => { };
            }

            var tokenSettings = new TokenSettings();
            tknSetup(tokenSettings);

            app.UseSwagger();

            return serveUi ? app.UseSwaggerUI(tokenSettings) : app;
        }

        private static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app, TokenSettings tokenSettings)
        {
            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = string.Empty;
                s.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
                if (!string.IsNullOrEmpty(tokenSettings.ClientId))
                {
                    s.OAuthClientId(tokenSettings.ClientId);
                }
            });
            return app;
        }

    }
}
