using ImageUploadService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageUploadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageService _imageUploadService;

        public ImageController(ImageService imageService)
        {
            _imageUploadService = imageService;
        }


        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _imageUploadService.UploadFileAsync(file);
            return result ? Ok() : BadRequest("Error");
        }

        [HttpGet("GetImages")]
        public async Task<IActionResult> GetImages()
        {
            var result = await _imageUploadService.GetAllImages();
            return Ok(result);
        }

        [HttpDelete("DeleteImage")]
        public async Task<IActionResult> DeleteImage([FromQuery] string id)
        {
            var result=await _imageUploadService.DeleteImageAsync(id) ;
            return result? Ok():BadRequest("Error");
        }

        

        

    }
}
