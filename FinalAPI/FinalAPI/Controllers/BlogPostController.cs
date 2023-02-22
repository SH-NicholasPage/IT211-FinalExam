using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    public class BlogPostController : ControllerBase
    {
        private readonly ILogger<BlogPostController> _logger;

        public BlogPostController(ILogger<BlogPostController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of blog posts with the given IDs
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Blog post objects
        /// </returns>
        /// <response code="400">If there are no blog posts with the given IDs</response>
        [HttpGet(Name = "GetBlogPosts")]
        [ActionName("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Object Get([Required][FromQuery] int[] id)
        {
            List<BlogPost> toReturn = new List<BlogPost>();
            //LINQ is so cool :')
            id.Where(x => TempDBContext.BlogPosts.Any(y => y.Id == x)).ToList().ForEach(x => toReturn.Add(TempDBContext.BlogPosts.Where(y => y.Id == x).First()));
            return toReturn.Count > 0 ? Ok(toReturn) : BadRequest("No blog posts found with the given IDs");
        }

        /// <summary>
        /// Gets a blog post with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// One blog post object
        /// </returns>
        /// <response code="400">If there is no blog post with the given ID</response>
        [HttpGet(Name = "GetOneBlogPost")]
        [ActionName("GetOne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Object GetOne([Required] int id)
        {
            if (TempDBContext.BlogPosts.Any(x => x.Id == id) == false)
            {
                return BadRequest($"There is no post with the ID {id}.");
            }
            else
            {
                return Ok(TempDBContext.BlogPosts.Where(x => x.Id == id).ToList());
            }
        }

        /// <summary>
        /// Gets all blog posts
        /// </summary>
        /// <returns>
        /// All blog posts objects on the server
        /// </returns>
        [HttpGet(Name = "GetAllBlogPosts")]
        [ActionName("GetAll")]
        public Object GetAll()
        {
            return Ok(TempDBContext.BlogPosts);
        }
    }
}