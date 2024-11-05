using AutoMapper;
using Domain.Entities;
using Shared.DTOs;

namespace Shared.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<Hotel, HotelDto>().ReverseMap();
        }
    }
}
