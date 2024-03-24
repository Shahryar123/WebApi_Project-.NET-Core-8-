using Microsoft.EntityFrameworkCore;
using Practice.API.Models.Domain;

namespace Practice.API.Data
{
    public class PracticeDbContext : DbContext
    {
        public PracticeDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficuilty> Difficuilties { get; set; }
        
    }
}
