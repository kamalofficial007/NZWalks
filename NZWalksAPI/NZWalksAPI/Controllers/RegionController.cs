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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
           var region = await regionRepository.GetAsync(id);
            if(region==null)
                return NotFound();
        var regionList = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionList);
        }

        [HttpPost]

        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request to Domain model
            var region = new Models.Domain.Region()
            {
                Area = addRegionRequest.Area,
                Code = addRegionRequest.Code,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population


            };

            //Pass details to repo
             region =  await regionRepository.AddAsync(region);
            //Convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Area = region.Area,
                Code = region.Code,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population

            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from db
            var region = await regionRepository.DeleteAsync(id);

            //if not found
            if (region == null)
            {
                return NotFound();
            }

            //Convert responce back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Area = region.Area,
                Code = region.Code,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population

            };
            //return Ok response
            return Ok(regionDTO)
;        }


        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to Domain model
            var region = new Models.Domain.Region()
            {
                Area = updateRegionRequest.Area,
                Code = updateRegionRequest.Code,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population


            };

            //update region using repo
            region = await regionRepository.UpdateAsync(id, region);
            //if null not found
            if(region == null)
                return NotFound();


            //convert domain into DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Area = region.Area,
                Code = region.Code,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population

            };

            //return ok reponse

            return Ok(regionDTO);

        }
    }
}
