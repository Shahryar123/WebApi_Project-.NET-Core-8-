using AutoMapper;
using Practice.API.Models.Domain;
using Practice.API.Models.DTO;

namespace Practice.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //MAP REGION MODEL TO DTO AND VISE VERSA
            CreateMap<Region , RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();
        }
    }
}
