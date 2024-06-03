using FinalAPI.Models;
using FinalAPI.Services;
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
        private TempDBService TempDBService { get; init; }

        public BlogPostController(ILogger<BlogPostController> logger, TempDBService tempDBService)
        {
            _logger = logger;
            TempDBService = tempDBService;
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
            List<BlogPost> toReturn = TempDBService.BlogPosts
                .Where(b => id.Contains(b.Id))
                .ToList();
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
            if (TempDBService.BlogPosts.Any(x => x.Id == id) == false)
            {
                return BadRequest($"There is no post with the ID {id}.");
            }
            else
            {
                return Ok(TempDBService.BlogPosts.Where(x => x.Id == id).ToList());
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
            return Ok(TempDBService.BlogPosts);
        }

        /// <summary>
        /// Creates a new blog post
        /// </summary>
        /// <param name="posterId"></param>
        /// <param name="body"></param>
        /// <returns>
        /// Returns the newly created blog post
        /// </returns>
        [HttpPost(Name = "CreateNewPost")]
        [ActionName("CreatePost")]
        public Object CreatePost([Required] int posterId, [Required] String body)
        {
            Profile? poster = TempDBService.Profiles.FirstOrDefault(p => p.Id == posterId);
            if (poster == null)
            {
                return BadRequest($"There is no profile with the ID {posterId}.");
            }

            BlogPost newPost = new BlogPost
            {
                Id = TempDBService.BlogPosts.Max(p => p.Id) + 1,
                PosterAccount = poster,
                Body = body,
                DateTimePosted = DateTime.Now,
                Likes = 0,
                Comments = []
            };

            TempDBService.BlogPosts.Add(newPost);
            return Ok(newPost);
        }

        /// <summary>
        /// Creates a new comment on a blog post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="comment"></param>
        /// <returns>
        /// Returns the blog post with the new comment
        /// </returns>
        [HttpPost(Name = "CreateNewComment")]
        [ActionName("CreateComment")]
        public Object CreateComment([Required] int postId, [Required] String comment)
        {
            BlogPost? post = TempDBService.BlogPosts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return BadRequest($"There is no post with the ID {postId}.");
            }

            post.Comments.Add(comment);
            return Ok(post);
        }

        /// <summary>
        /// Likes a blog post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>
        /// Returns the blog post with the updated like count
        /// </returns>
        [HttpPut(Name = "LikePost")]
        [ActionName("LikePost")]
        public Object LikePost([Required] int postId)
        {
            BlogPost? post = TempDBService.BlogPosts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return BadRequest($"There is no post with the ID {postId}.");
            }

            post.Likes++;
            return Ok(post);
        }

        /// <summary>
        /// Edits a blog post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="newBody"></param>
        /// <returns>
        /// Returns the blog post with the updated body
        /// </returns>
        [HttpPut(Name = "EditOnePost")]
        [ActionName("EditPost")]
        public Object EditPost([Required] int postId, [Required] String newBody)
        {
            BlogPost? post = TempDBService.BlogPosts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return BadRequest($"There is no post with the ID {postId}.");
            }

            post.Body = newBody;
            return Ok(post);
        }

        /// <summary>
        /// Deletes a blog post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>
        /// Returns the deleted blog post
        /// </returns>
        [HttpDelete(Name = "DeleteOnePost")]
        [ActionName("DeletePost")]
        public Object DeletePost([Required] int postId)
        {
            BlogPost? post = TempDBService.BlogPosts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return BadRequest($"There is no post with the ID {postId}.");
            }

            TempDBService.BlogPosts.Remove(post);
            return Ok(post);
        }

        /// <summary>
        /// Deletes a comment on a blog post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="commentIndex"></param>
        /// <returns>
        /// Returns the deleted comment
        /// </returns>
        [HttpDelete(Name = "DeleteOneComment")]
        [ActionName("DeleteComment")]
        public Object DeleteComment([Required] int postId, [Required] int commentIndex)
        {
            BlogPost? post = TempDBService.BlogPosts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return BadRequest($"There is no post with the ID {postId}.");
            }

            if (commentIndex < 0 || commentIndex >= post.Comments.Count)
            {
                return BadRequest($"There is no comment at index {commentIndex}.");
            }

            String comment = post.Comments[commentIndex];
            post.Comments.RemoveAt(commentIndex);
            return Ok(comment);
        }
    }
}