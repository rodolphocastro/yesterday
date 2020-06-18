using System;
using System.Collections.Generic;

namespace Api.Auth
{
    /// <summary>
    /// Settings for the API's JWT Token.
    /// </summary>
    public class TokenSettings
    {
        /// <summary>
        /// Key to access the Settings within IConfiguration.
        /// </summary>
        public const string TokenSettingsKey = "JwtSettings";

        public TokenSettings() { }

        public string Audience { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public IEnumerable<string> Issuers { get; set; } = new List<string>();
        public string ClientId { get; set; } = string.Empty;
        public string TokenAddress { get; set; } = string.Empty;
        public string AuthorizeAddress { get; set; } = string.Empty;
        public string WellKnownAddress { get; set; } = string.Empty;
        public Uri TokenUrl => new Uri(TokenAddress);
        public Uri AuthorizeUrl => new Uri(AuthorizeAddress);
        public Uri DiscoveryUrl => new Uri(WellKnownAddress);
    }
}
