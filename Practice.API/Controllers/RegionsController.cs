using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.API.Data;
using Practice.API.Models.Domain;
using Practice.API.Models.DTO;
using Practice.API.Repository;

namespace Practice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly PracticeDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(PracticeDbContext dbContext , IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map RegionModel to RegionDto
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionsDto);
        }
        
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionsDomain = await regionRepository.GetbyIdAsync(id);
            
            if (regionsDomain == null) { return NotFound(); }

            var regionsDto = mapper.Map<RegionDto>(regionsDomain);
            return Ok(regionsDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionRequestDto)
        {
            //from dto to domain map
            var addregion_domain = mapper.Map<Region>(regionRequestDto);

            addregion_domain = await regionRepository.CreateAsync(addregion_domain);
            //from domain to dto map
            var addregiondto = mapper.Map<RegionDto>(addregion_domain);

            return CreatedAtAction(nameof(GetById), new { id = addregiondto.Id }, addregiondto);
        }
       
        #region CreateRegion without automapper
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionRequestDto)
        //{
        //    var addregion_domain = new Region()
        //    {
        //        Code = regionRequestDto.Code,
        //        Name = regionRequestDto.Name,
        //        RegionImageUrl = regionRequestDto.RegionImageUrl,
        //    };

        //    addregion_domain = await regionRepository.CreateAsync(addregion_domain);

        //    var addregiondto = new RegionDto()
        //    {
        //        Id = addregion_domain.Id,
        //        Code = addregion_domain.Code,
        //        Name = addregion_domain.Name,
        //        RegionImageUrl = addregion_domain.RegionImageUrl,
        //    };

        //    return CreatedAtAction(nameof(GetById), new { id = addregiondto.Id }, addregiondto);
        //}
        #endregion
        
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequest)
        {
            var domainregion = mapper.Map<Region>(updateRegionRequest);

            domainregion = await regionRepository.UpdateAsync(id , domainregion);

            if (domainregion == null) { NotFound(); }
            
            var updatedregion_dto = mapper.Map<RegionDto>(domainregion);
            return Ok(updatedregion_dto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            var domainregion = await regionRepository.DeleteAsync(id);

            if(domainregion == null) { return NotFound(); }

            var deletedregion_dto = mapper.Map<RegionDto>(domainregion);

            return Ok(deletedregion_dto);
        }
    }
}
