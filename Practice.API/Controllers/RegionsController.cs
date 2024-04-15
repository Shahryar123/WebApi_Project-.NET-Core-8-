using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.API.CustomActionFilters;
using Practice.API.Data;
using Practice.API.Models.Domain;
using Practice.API.Models.DTO;
using Practice.API.Repository;
using System.Text.Json;

namespace Practice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository,IMapper mapper, ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Used LogInformation because declare in program.cs
                logger.LogInformation("Program Execution Started");
                var regionsDomain = await regionRepository.GetAllAsync();

                //Map RegionModel to RegionDto
                var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
                logger.LogInformation($"Get the Regions data: {JsonSerializer.Serialize(regionsDomain)}");
                return Ok(regionsDto);
            }
            catch(Exception ex)
            {
                logger.LogError(ex , ex.Message);
                throw;
            }
        }
        
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionsDomain = await regionRepository.GetbyIdAsync(id);
            
            if (regionsDomain == null) { return NotFound(); }

            var regionsDto = mapper.Map<RegionDto>(regionsDomain);
            return Ok(regionsDto);
        }
        
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto regionRequestDto)
        {
            // This is Model State validation which check the data annotation in dtos
            //if validate model is used at data annotation then this would not be used

            //if (ModelState.IsValid)
            //{
                //from dto to domain map
                var addregion_domain = mapper.Map<Region>(regionRequestDto);

                addregion_domain = await regionRepository.CreateAsync(addregion_domain);
                //from domain to dto map
                var addregiondto = mapper.Map<RegionDto>(addregion_domain);

                return CreatedAtAction(nameof(GetById), new { id = addregiondto.Id }, addregiondto);
            //}
            //else
            //{
            //  return BadRequest(ModelState);
            //}
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
        [ValidateModel]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequest)
        {
            
            var domainregion = mapper.Map<Region>(updateRegionRequest);

            domainregion = await regionRepository.UpdateAsync(id, domainregion);

            if (domainregion == null) { NotFound(); }

            var updatedregion_dto = mapper.Map<RegionDto>(domainregion);
            return Ok(updatedregion_dto);
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            var domainregion = await regionRepository.DeleteAsync(id);

            if(domainregion == null) { return NotFound(); }

            var deletedregion_dto = mapper.Map<RegionDto>(domainregion);

            return Ok(deletedregion_dto);
        }
    }
}
