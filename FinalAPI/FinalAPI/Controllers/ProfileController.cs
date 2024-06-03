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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Object GetAll()
        {
            return Ok(TempDBService.Profiles);
        }

        /// <summary>
        /// Creates a new profile
        /// </summary>
        /// <param name="name"></param>
        /// <param name="biography"></param>
        /// <param name="email"></param>
        /// <returns>
        /// Returns the newly created profile
        /// </returns>
        [HttpPost(Name = "CreateNewProfile")]
        [ActionName("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Object Create([Required] String name, [Required] String biography, [Required] String email)
        {
            int newId = TempDBService.Profiles.Max(p => p.Id) + 1;

            Profile newProfile = new Profile
            {
                Id = newId,
                Name = name,
                Biography = biography,
                Email = email
            };
            TempDBService.Profiles.Add(newProfile);
            return CreatedAtAction(nameof(GetOne), new { id = newId }, newProfile);
        }

        /// <summary>
        /// Edits a profile with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="biography"></param>
        /// <param name="email"></param>
        /// <returns>
        /// Returns the edited profile
        /// </returns>
        [HttpPut(Name = "EditOneProfile")]
        [ActionName("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Object Edit([Required] int id, [Required] String name, [Required] String biography, [Required] String email)
        {
            Profile? profile = TempDBService.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                return BadRequest($"There is no profile with the ID {id}.");
            }
            profile.Name = name;
            profile.Biography = biography;
            profile.Email = email;
            return Ok(profile);
        }

        /// <summary>
        /// Edits the name of a profile with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns>
        /// Returns the edited profile
        /// </returns>
        [HttpPut(Name = "EditOneProfileName")]
        [ActionName("EditName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Object EditName([Required] int id, [Required] String name)
        {
            Profile? profile = TempDBService.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                return BadRequest($"There is no profile with the ID {id}.");
            }
            profile.Name = name;
            return Ok(profile);
        }

        /// <summary>
        /// Edits the biography of a profile with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="biography"></param>
        /// <returns>
        /// Returns the edited profile
        /// </returns>
        [HttpPut(Name = "EditOneProfileBiography")]
        [ActionName("EditBiography")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Object EditBiography([Required] int id, [Required] String biography)
        {
            Profile? profile = TempDBService.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                return BadRequest($"There is no profile with the ID {id}.");
            }
            profile.Biography = biography;
            return Ok(profile);
        }

        /// <summary>
        /// Edits the email of a profile with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns>
        /// Returns the edited profile
        /// </returns>
        [HttpPut(Name = "EditOneProfileEmail")]
        [ActionName("EditEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Object EditEmail([Required] int id, [Required] String email)
        {
            Profile? profile = TempDBService.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                return BadRequest($"There is no profile with the ID {id}.");
            }
            profile.Email = email;
            return Ok(profile);
        }

        /// <summary>
        /// Deletes a profile with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Returns the deleted profile
        /// </returns>
        [HttpDelete(Name = "DeleteOneProfile")]
        [ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Object Delete([Required] int id)
        {
            Profile? profile = TempDBService.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                return BadRequest($"There is no profile with the ID {id}.");
            }
            TempDBService.Profiles.Remove(profile);
            return Ok(profile);
        }
    }
}