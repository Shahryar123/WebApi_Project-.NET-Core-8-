using System.ComponentModel.DataAnnotations;

namespace Practice.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "The length should be maximum 100")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "The length should be maximum 1000")]
        public string Description { get; set; }

        [Required]
        [Range(0, 10)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
