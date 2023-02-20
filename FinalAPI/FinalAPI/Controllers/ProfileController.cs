using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<BlogPostController> _logger;

        public ProfileController(ILogger<BlogPostController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProfiles")]
        public Object Get(int? id)
        {
            if (id == null)
            {
                return Ok(TempDBContext.Profiles);
            }
            else if (TempDBContext.Profiles.Any(x => x.Id == id) == false)
            {
                return BadRequest($"The ID {id} does not exist.");
            }
            else
            {
                return Ok(TempDBContext.Profiles.Where(x => x.Id == id).ToList());
            }
        }
    }
}