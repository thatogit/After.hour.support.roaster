using After.hour.support.roaster.api.Model;
using After.hour.support.roaster.api.Model.Dto;
using AutoMapper;

namespace After.hour.support.roaster.api
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Roaster, RoasterCreateDto>().ReverseMap();
            CreateMap<Roaster, RoasterCreateDto>().ReverseMap();
            CreateMap<Roaster, RoasterUpdateDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDto>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();

            CreateMap<Team, TeamDto>().ReverseMap();
            //CreateMap<Team, TeamDto>().ForAllMembers(d=>d.Equals(true));
            CreateMap<Team, TeamCreateDto>().ReverseMap();
            CreateMap<Team, TeamUpdateDto>().ReverseMap();
        }
    }
}
