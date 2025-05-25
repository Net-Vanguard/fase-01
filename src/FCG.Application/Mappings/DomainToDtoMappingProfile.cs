using AutoMapper;
using FCG.Application.DTOs;
using FCG.Domain.Entities;

namespace FCG.Application.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address));

            CreateMap<Game, GameResponse>();
        }
    }
}
