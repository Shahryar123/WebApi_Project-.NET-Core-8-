using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.API.CustomActionFilters;
using Practice.API.Models.Domain;
using Practice.API.Models.DTO;
using Practice.API.Repository;

namespace Practice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalksRepository walksRepository , IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }

        // FILTERING PAGINATION AND SORTING DONE HERE

        // api/walks/filterOn=Name&filterQuery=track&sortBy=Length&isAscending=true
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn = null ,[FromQuery] string? filterQuery = null,
            [FromQuery] string? sortBy = null , [FromQuery] bool? isAscending = null,
            [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomain = await walksRepository
                .GetAllAsync(filterOn , filterQuery , sortBy , isAscending??true , pageNo , pageSize);

            //Map WalksModel to RegionDto
            var walksDto = mapper.Map<List<WalkDto>>(walksDomain);

            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walksDomain = await walksRepository.GetbyIdAsync(id);

            if (walksDomain == null) { return NotFound(); }

            var walksDto = mapper.Map<WalkDto>(walksDomain);

            return Ok(walksDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto walkRequestDto)
        {
            //if validate model is used at data annotation then this would not be used
            //if (ModelState.IsValid)
            //{
                //from dto to domain map
                var addwalks_domain = mapper.Map<Walk>(walkRequestDto);

                addwalks_domain = await walksRepository.CreateAsync(addwalks_domain);
                //from domain to dto map
                var addwalksdto = mapper.Map<WalkDto>(addwalks_domain);

                return CreatedAtAction(nameof(GetById), new { id = addwalksdto.Id }, addwalksdto);
            //}
            //else
            //{
            //   return BadRequest(ModelState);
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
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequest)
        {
            var domainwalk = mapper.Map<Walk>(updateWalkRequest);

            domainwalk = await walksRepository.UpdateAsync(id, domainwalk);

            if (domainwalk == null) { NotFound(); }

            var updatedwalk_dto = mapper.Map<WalkDto>(domainwalk);
            return Ok(updatedwalk_dto);
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domainregion = await walksRepository.DeleteAsync(id);

            if (domainregion == null) { return NotFound(); }

            var deletedregion_dto = mapper.Map<WalkDto>(domainregion);

            return Ok(deletedregion_dto);
        }
    }
}
