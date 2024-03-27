using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.API.Models.DTO;

namespace Practice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public ImageController()
        {
            
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto uploadRequestDto)
        {

        }
    }
}
