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

        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalkDbContext1.Regions.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalkDbContext1.AddAsync(region);
            await nZWalkDbContext1.SaveChangesAsync();
            return region;

        }

        public async Task<Region> DeleteAsync(Guid id)
        {
           var region= await nZWalkDbContext1.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (region == null)
                return null;
            nZWalkDbContext1.Regions.Remove(region);
            await nZWalkDbContext1.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
           var existingregion = await nZWalkDbContext1.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingregion == null)
                return null;
            existingregion.Area = region.Area;
            existingregion.Code = region.Code;
            existingregion.Name = region.Name;
            existingregion.Lat=region.Lat;
            existingregion.Long = region.Long;
            existingregion.Population = region.Population;
            await nZWalkDbContext1.SaveChangesAsync();
            return existingregion;
        }
    }
}
