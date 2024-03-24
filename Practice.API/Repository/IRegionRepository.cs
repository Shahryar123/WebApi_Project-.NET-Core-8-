using Practice.API.Models.Domain;
using Practice.API.Models.DTO;

namespace Practice.API.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetbyIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id , Region region);

        Task<Region?> DeleteAsync(Guid id);
    }
}
