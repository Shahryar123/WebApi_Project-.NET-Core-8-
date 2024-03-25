using Practice.API.Models.Domain;

namespace Practice.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //public Guid DifficultyId { get; set; }
        //public Guid RegionId { get; set; }

        // Naming should be same in dto and in domain model
        public DifficuiltyDto Difficulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
