﻿using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces
{
    public interface IDailyWorkScheduleRepository
    {
        /// <summary>
        /// GetByDepartment includes child departments.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<IDictionary<Employee, IEnumerable<DailyWorkSchedule>>> GetByDepartment(string departmentId, int year, int month);

        Task<IDictionary<Employee, IEnumerable<DailyWorkSchedule>>> GetAll(int year, int month);
    }
}
