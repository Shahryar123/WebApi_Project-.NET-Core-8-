using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Practice.API.Data;
using Practice.API.Models.Domain;
using Practice.API.Models.DTO;

namespace Practice.API.Repository
{
    public class SqlWalkRepository : IWalksRepository
    {
        private readonly PracticeDbContext dbContext;

        public SqlWalkRepository(PracticeDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }


        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include(x=>x.Difficulty).Include(y=>y.Region).ToListAsync();
        }

        public async Task<Walk?> GetbyIdAsync(Guid id)
        {
            return await dbContext.Walks.Include(x => x.Difficulty).Include(y => y.Region).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id , Walk walk)
        {
            var existing_walk = await dbContext.Walks.Include(x => x.Difficulty).Include(y => y.Region).FirstOrDefaultAsync(z => z.Id == id);
            if(existing_walk == null) { return null;  }

            existing_walk.Name = walk.Name;
            existing_walk.Description = walk.Description;
            existing_walk.LengthInKm = walk.LengthInKm;
            existing_walk.WalkImageUrl = walk.WalkImageUrl;
            existing_walk.DifficultyId = walk.DifficultyId;
            existing_walk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return existing_walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var deleted_walk = await dbContext.Walks.Include(x=>x.Difficulty).Include(y=>y.Region).FirstOrDefaultAsync(z=>z.Id == id);
            if (deleted_walk == null)
            { return null; }

            dbContext.Remove(deleted_walk);
            await dbContext.SaveChangesAsync();

            return deleted_walk;
        }

    }
}
