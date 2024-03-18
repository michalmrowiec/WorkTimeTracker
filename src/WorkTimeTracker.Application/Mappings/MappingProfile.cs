using AutoMapper;
using WorkTimeTracker.Application.DailyWorkSchedules;
using WorkTimeTracker.Application.Departments;
using WorkTimeTracker.Application.Departments.Queries;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Application.Employees.Queries.GetEmployeeDetails;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Application.Mappings
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dto => dto.Department,
                opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dto => dto.Department,
                opt => opt.MapFrom(src => src.Department));

            CreateMap<DailyWorkSchedule, DailyWorkScheduleDto>()
                .ReverseMap();

            CreateMap<Department, DepartmentDto>()
                .ReverseMap();

            CreateMap<Department, DepartmentDetailsDto>()
                .ForMember(dto => dto.ParentDepartmentName,
                opt => opt.MapFrom(src => src.ParentDepartment != null ? src.ParentDepartment.Name : string.Empty));


        }
    }
}
