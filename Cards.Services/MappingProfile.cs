using AutoMapper;
using Cards.Domain.Entities;
using Cards.Services.Dtos;

namespace Cards.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, CardDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.CreatedByAppUser, opt => opt.MapFrom(src => src.AppUser.Email));

            CreateMap<CardForCreationDto, Card>();

            CreateMap<CardForUpdateDto, Card>();

            CreateMap<UserForRegistrationDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

        }
    }
}