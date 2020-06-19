using System.Threading.Tasks;

using Api.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers.Profiles
{
    [Route("/api/v1/[controller]")]
    public class ProfilesController : YesterdayControllerBase
    {
        public ProfilesController(ILogger<YesterdayControllerBase> logger) : base(logger)
        {
        }

        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Profile>> GetMyProfile()
        {
            // TODO: Actually connect to a database of some sort
            // If the user doesn't have a profile a new Profile should be created in its behalf
            return Ok(new Profile { Id = Username, Nickname = "It's a meee, Profilio!" });
        }

        [Authorize]
        [HttpPut("me")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMyProfile(Profile updatedProfile)
        {
            // TODO: Actually fetch the original from the database and then update it
            return NoContent();
        }
    }
}
