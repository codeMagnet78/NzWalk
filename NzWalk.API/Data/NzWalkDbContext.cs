using Microsoft.EntityFrameworkCore;
using NzWalk.API.Models.Domain;

namespace NzWalk.API.Data
{
    public class NzWalkDbContext : DbContext
    {
        public NzWalkDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        //Mapping with the Domain Models
        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

    }
}
