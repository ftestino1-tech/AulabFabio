using Microsoft.AspNetCore.Mvc; 
using Blog.Web.Repositories; 

namespace Blog.Web.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository; 

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository; 
        }

        [HttpPost]
        public IActionResult Upload([FromForm] IFormFile file)
        {
            var imageUrl = _imageRepository.Upload(file); 

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Problem(
                    detail: "Image upload was not successful.", 
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }

            return new JsonResult(new { link = imageUrl });
        }
    }

}