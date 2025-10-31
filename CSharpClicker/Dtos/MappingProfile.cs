using AutoMapper;
using CSharpClicker.Domain;

namespace CSharpClicker.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserInfoDto>();
        CreateMap<UserBoost, UserBoostDto>();
        CreateMap<Boost, BoostDto>();
    }
}
