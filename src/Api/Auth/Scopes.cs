using System.Collections.Generic;

namespace Api.Auth
{
    /// <summary>
    /// Scopes supported by the API.
    /// </summary>
    internal static class Scopes
    {
        internal static class Manage
        {
            public const string Profile = "manage:profile";
            public const string Projects = "manage:projects";
        }

        internal static IDictionary<string, string> AllScopes { get; } = new Dictionary<string, string>
        {
            { Manage.Profile, "Allows the application to manage your Yesterday Profile" },
            { Manage.Projects, "Allows the application to manage your Projects" }
        };
    }
}
