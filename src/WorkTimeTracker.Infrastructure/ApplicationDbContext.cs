﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DailyWorkSchedule> DailyWorkSchedules { get; set; }
        public DbSet<ActionTime> ActionTimes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentManager> DepartmentManagers { get; set; }
        public DbSet<Holiday> Holidays { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DepartmentManager>()
                .HasKey(dm => new { dm.DepartmentId, dm.ManagerId });

            builder.Entity<DailyWorkSchedule>()
                .HasOne(d => d.Employee)
                .WithMany(e => e.DailyWorkSchedules)
                .HasForeignKey(d => d.EmployeeId);

            builder.Entity<DailyWorkSchedule>()
                .Property(d => d.TypeOfDay)
                .HasConversion<string>();
        }
    }
}
