using AutoMapper;
using Domain.Entities;
using Shared.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
