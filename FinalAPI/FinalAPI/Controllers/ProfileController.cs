using FinalAPI.Models;
using FinalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<BlogPostController> _logger;
        private TempDBService TempDBService { get; init; }

        public ProfileController(ILogger<BlogPostController> logger, TempDBService tempDBService)
        {
            _logger = logger;
            TempDBService = tempDBService;
        }

        /// <summary>
        /// Gets a list of profiles with the given IDs
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Profile objects
        /// </returns>
        /// <response code="400">If there are no profiles with the given IDs</response>
        [HttpGet(Name = "GetProfiles")]
        [ActionName("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Object Get([Required][FromQuery] int[] id)
        {
            List<Profile> toReturn = TempDBService.Profiles
                .Where(p => id.Contains(p.Id))
                .ToList();
            return toReturn.Count > 0 ? Ok(toReturn) : BadRequest("No profiles found with the given IDs");
        }

        /// <summary>
        /// Gets a profile with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// One profile object
        /// </returns>
        /// <response code="400">If there is no profile with the given ID</response>
        [HttpGet(Name = "GetOneProfile")]
        [ActionName("GetOne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Object GetOne([Required] int id)
        {
            if (TempDBService.Profiles.Any(x => x.Id == id) == false)
            {
                return BadRequest($"There is no profile with the ID {id}.");
            }
            else
            {
                return Ok(TempDBService.Profiles.Where(x => x.Id == id).ToList());
            }
        }

        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns>
        /// All profile objects
        /// </returns>
        [HttpGet(Name = "GetAllProfiles")]
        [ActionName("GetAll")]
        public Object GetAll()
        {
            return Ok(TempDBService.Profiles);
        }
    }
}