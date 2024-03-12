using AutoMapper;
using Cards.Domain.Entities;
using Cards.Services.Dtos;

namespace Cards.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CardDto, Card >()
            .ForMember(dest => dest.CardId, opt => opt.Ignore()) 
            .ForMember(dest => dest.Status, opt => opt.Ignore()) 
            .ForMember(dest => dest.AppUser, opt => opt.Ignore());

        CreateMap<Card, CardDto>();

        CreateMap<AppUser, AppUserDto>();
    }
}
