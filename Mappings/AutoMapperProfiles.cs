using AutoMapper;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // ReverseMap: enables mapping on other way
            CreateMap<RegionDto, Region>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<CreateWalkDto, Walk>().ReverseMap();
            CreateMap<WalkDto, Walk>().ReverseMap();

            CreateMap<DifficultyDto, Difficulty>().ReverseMap();
        }
    }
}
