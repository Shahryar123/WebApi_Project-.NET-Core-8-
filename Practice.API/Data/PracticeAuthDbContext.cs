using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Practice.API.Models.Domain;

namespace Practice.API.Data
{
    public class PracticeAuthDbContext : IdentityDbContext
    {
        public PracticeAuthDbContext(DbContextOptions<PracticeAuthDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }


        // WE HAVE TWO ROLES THAT WE INJECT FROM HERE 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            var readerRoleid = "bf0acce0 - d5be - 42cc - a5eb - 71dec7688180";
            var writerRoleid = "5c9ec6f7 - 7ac6 - 4b3f - bd92 - 56f53dca9507";
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = readerRoleid,
                    Name = "Reader",
                    ConcurrencyStamp = readerRoleid,
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole()
                {
                    Id = writerRoleid,
                    Name = "Writer",
                    ConcurrencyStamp = writerRoleid,
                    NormalizedName = "Writer".ToUpper()
                },
                
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);



        }
    }
}
