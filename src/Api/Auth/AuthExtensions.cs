using System;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Auth
{
    /// <summary>
    /// Extensions for adding and setting up Authentication and Authorization on the API.
    /// </summary>
    public static class AuthExtensions
    {
        /// <summary>
        /// Adds services to handle Authentication and Authorization.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="tkAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuth(this IServiceCollection services, Action<TokenSettings> tkAction = null)
        {
            if (tkAction is null)
            {
                tkAction = (_) => { };
            }

            var tokenSettings = new TokenSettings();
            tkAction(tokenSettings);

            return services.AddAuth(tokenSettings);
        }

        private static IServiceCollection AddAuth(this IServiceCollection services, TokenSettings settings)
        {
            services
                .AddAuthentication(s =>
                {
                    s.DefaultAuthenticateScheme = s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(s =>
                {
                    s.SaveToken = true;
                    s.Authority = settings.Authority;
                    s.Audience = settings.Audience;
                    s.MetadataAddress = string.IsNullOrWhiteSpace(settings.WellKnownAddress) ? null : settings.WellKnownAddress;
                });
            return services;
        }
    }
}
