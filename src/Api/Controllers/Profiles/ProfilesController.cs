using System;
using System.Threading.Tasks;

using Api.Models;
using Api.Storage;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers.Profiles
{
    [Route("/api/v1/[controller]")]
    public class ProfilesController : YesterdayControllerBase
    {
        private readonly IStorage<Profile> _storage;

        public ProfilesController(IStorage<Profile> storage, ILogger<YesterdayControllerBase> logger = null) : base(logger)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Profile>> GetMyProfile()
        {
            var result = await _storage.GetAsync(p => p.Id == Username);
            if (result is null)
            {
                result = new Profile { Id = Username, Nickname = "A new user enters the Arena" };
                await _storage.InsertAsync(result);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPut("me")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMyProfile(Profile updatedProfile)
        {
            await _storage.UpdateAsync(p => p.Id == Username, updatedProfile);
            return NoContent();
        }
    }
}
