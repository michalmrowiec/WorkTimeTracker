using AutoMapper;
using WorkTimeTracker.Application.DailyWorkSchedules;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.Mappings
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();

            CreateMap<DailyWorkSchedule , DailyWorkScheduleDto>()
                .ReverseMap();

        }
    }
}
