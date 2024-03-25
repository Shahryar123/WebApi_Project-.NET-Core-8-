using Practice.API.Models.Domain;
using Practice.API.Models.DTO;

namespace Practice.API.Repository
{
    public interface IWalksRepository
    {
        Task<List<Walk>> GetAllAsync(
            string? filterOn = null , string? filterQuery = null ,
            string? sortBy = null , bool isAscending = true,
            int pageNo = 1 , int pageSize = 1000
            );

        Task<Walk?> GetbyIdAsync(Guid id);

        Task<Walk> CreateAsync(Walk walk);

        Task<Walk?> UpdateAsync(Guid id , Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}
