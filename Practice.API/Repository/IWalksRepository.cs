using Practice.API.Models.Domain;
using Practice.API.Models.DTO;

namespace Practice.API.Repository
{
    public interface IWalksRepository
    {
        Task<List<Walk>> GetAllAsync();

        Task<Walk?> GetbyIdAsync(Guid id);

        Task<Walk> CreateAsync(Walk walk);

        Task<Walk?> UpdateAsync(Guid id , Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}
