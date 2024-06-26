﻿using AutoMapper;
using WorkTimeTracker.Application.ActionTimes;
using WorkTimeTracker.Application.ActionTimes.Commands.UpdateActionTime;
using WorkTimeTracker.Application.DailyWorkSchedules;
using WorkTimeTracker.Application.DailyWorkSchedules.Commands.UpdateDailyWorkSchedule;
using WorkTimeTracker.Application.Departments;
using WorkTimeTracker.Application.Departments.Queries;
using WorkTimeTracker.Application.Employees;
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
                .ForMember(dto => dto.RealOvertime,
                opt => opt.MapFrom(src => TimeSpan.FromMinutes(src.RealOvertimeMinutes)))
                .ReverseMap();

            CreateMap<Department, DepartmentDto>()
                .ReverseMap();

            CreateMap<Department, DepartmentDetailsDto>()
                .ForMember(dto => dto.ParentDepartmentName,
                opt => opt.MapFrom(src => src.ParentDepartment != null ? src.ParentDepartment.Name : string.Empty));

            CreateMap<ActionTime, ActionTimeDto>()
                .ReverseMap();

            CreateMap<UpdateActionTimeCommand, ActionTime>();

            CreateMap<UpdateDailyWorkScheduleCommand, DailyWorkSchedule>();

            CreateMap<DailyWorkScheduleDto, UpdateDailyWorkScheduleCommand>();

        }
    }
}
