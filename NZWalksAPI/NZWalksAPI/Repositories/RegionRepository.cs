using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext1;

        public RegionRepository(NZWalkDbContext nZWalkDbContext1)
        {
            this.nZWalkDbContext1 = nZWalkDbContext1;
        }

       

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalkDbContext1.Regions.ToListAsync(); 
        }
    }
}
