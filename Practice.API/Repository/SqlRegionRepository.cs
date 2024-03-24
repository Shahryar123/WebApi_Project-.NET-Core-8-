using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Practice.API.Data;
using Practice.API.Models.Domain;
using Practice.API.Models.DTO;

namespace Practice.API.Repository
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly PracticeDbContext dbContext;

        public SqlRegionRepository(PracticeDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }


        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetbyIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id , Region region)
        {
            var existing_region = dbContext.Regions.Find(id);
            if(existing_region == null) { return null;  }

            existing_region.Code = region.Code;
            existing_region.Name = region.Name;
            existing_region.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existing_region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var region = await dbContext.Regions.FindAsync(id);
            if (region == null)
            { return null; }

            dbContext.Remove(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

    }
}
