using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.API.Models.Domain;
using Practice.API.Models.DTO;
using Practice.API.Repository;

namespace Practice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto uploadRequestDto)
        {
            ValidateFileUpload(uploadRequestDto);

            if(ModelState.IsValid)
            {
                //First convert the imagedto to image to save in db
                var imageDomainModel = new Image
                {
                    File = uploadRequestDto.File,
                    FileName = uploadRequestDto.Filename,
                    FileSizeInBytes = uploadRequestDto.File.Length,
                    FileDescription = uploadRequestDto.Filedescription,
                    FileExtension = Path.GetExtension(uploadRequestDto.File.FileName)
                };

                await imageRepository.Upload(imageDomainModel);
                
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto uploadRequestDto)
        {
            var allowedExtensions = new string[] { ".png", ".jpg", ".jpeg" };

            if(allowedExtensions.Contains(Path.GetExtension(uploadRequestDto.File.FileName)) == false)
            {
                ModelState.AddModelError("file", "Unsupported File Extension");
            }

            if(uploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file","File Size more than 10 MB");
            }
            
        }
    }
}
