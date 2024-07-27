using NzWalk.API.Models.Domain;

namespace NzWalk.API.Repositories
{
    public interface IRegionRepository
    {
        //Get the list of Region using Async 
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid Id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid Id, Region region);

        Task<Region?> DeleteAsync(Guid Id);
    }
}
