using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NzWalk.API.Data;
using NzWalk.API.Models.Domain;

namespace NzWalk.API.Repositories
{
    //we have to implement the IRegionRepository to we created this class
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NzWalkDbContext dbContext;

        //Injecting the Dbcontext 
        public SqlRegionRepository(NzWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
           await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid Id)
        {
            var regionExits =  await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);
            if (regionExits == null) { return null; }

            dbContext.Regions.Remove(regionExits);
            await dbContext.SaveChangesAsync();
            return regionExits;
        }

        //This sql repository is now fetching the list of region 
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid Id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);
        }

        public async Task<Region?> UpdateAsync(Guid Id, Region region)
        {
            var regionExits =  await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);

            if (regionExits == null) { return null; }

            regionExits.Code = region.Code;
            regionExits.Name = region.Name;
            regionExits.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return regionExits;

        }
    }
}
