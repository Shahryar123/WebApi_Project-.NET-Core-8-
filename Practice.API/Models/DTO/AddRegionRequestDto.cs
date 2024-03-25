using System.ComponentModel.DataAnnotations;

namespace Practice.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        //These are data annotations for model state validation
        [Required]
        [MaxLength(100 , ErrorMessage = "The Length of the name has to 100 charachters") ]
        public string Name { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "The Length of the code has to 3 charachters")]
        public string Code { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
