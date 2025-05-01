using AutoMapper;
using SalonBooking.DTOs;
using SalonBooking.Models;

namespace SalonBooking.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
