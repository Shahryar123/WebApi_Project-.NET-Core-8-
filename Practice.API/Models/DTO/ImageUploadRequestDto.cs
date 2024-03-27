using System.ComponentModel.DataAnnotations;

namespace Practice.API.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string Filename { get; set; }
        public string? Filedescription { get; set; }


    }
}
