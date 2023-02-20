using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly ILogger<BlogPostController> _logger;

        public BlogPostController(ILogger<BlogPostController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBlogPosts")]
        public Object Get(int? id)
        {
            if (id == null)
            {
                return Ok(TempDBContext.BlogPosts);
            }
            else if (TempDBContext.BlogPosts.Any(x => x.Id == id) == false)
            {
                return BadRequest($"The ID {id} does not exist.");
            }
            else
            {
                return Ok(TempDBContext.BlogPosts.Where(x => x.Id == id).ToList());
            }
        }
    }
}