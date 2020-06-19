using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    /// <summary>
    /// Controller containing common logic and dependencies for the API.
    /// </summary>
    [ApiController]
    public abstract class YesterdayControllerBase : ControllerBase
    {
        protected ILogger<YesterdayControllerBase> Logger { get; }

        protected YesterdayControllerBase(ILogger<YesterdayControllerBase> logger = null)
        {
            Logger = logger;
        }

        /// <summary>
        /// Shorthand property to access the User's Name.
        /// </summary>
        protected string Username => HttpContext.User.Identity.IsAuthenticated ?
            HttpContext.User.Identity.Name
            : string.Empty;
    }
}
