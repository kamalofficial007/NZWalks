using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var region = await regionRepository.GetAllAsync();

            //DTO 
            //var regionList = new List<Models.DTO.Region>();
            //region.ToList().ForEach(reg =>
            //{
            //    var item = new Models.DTO.Region()
            //    {
            //        Area = reg.Area,
            //        Code = reg.Code,
            //        Id = reg.Id,
            //        Lat = reg.Lat,
            //        Long = reg.Long,
            //        Name = reg.Name,
            //        Population = reg.Population

            //    };
            //    regionList.Add(item);
            //});
            var regionList = mapper.Map<List<Models.DTO.Region>>(region);

            return Ok(regionList);
        }
    }
}
