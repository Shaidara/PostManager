using Microsoft.AspNetCore.Mvc;
using PostManager.Core.Interfaces.Services;
using PostManager.Core.Models;
using PostManager.Core.Payload;

namespace PostManager.Controllers
{
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService) 
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostByFilter(string tags, string sortBy = "id", string direction = "asc")
        {
            try
            {
                var response = await _postService.GetPostsByQueryParams(tags, sortBy, direction);
                return Ok(new ResponsePayload<IList<Post>> 
                { 
                    Data = response, 
                    Count = response.Count 
                });
            }
            catch (ArgumentException ex)
            {
                var error = new ErrorPayload
                {
                    ErrorMessage = ex.Message,
                    Status = StatusCodes.Status400BadRequest
                };
                return BadRequest(error);
            }
            catch (Exception ex)
            {
                
                var error = new ErrorPayload
                {
                    ErrorMessage = ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
    }
}
