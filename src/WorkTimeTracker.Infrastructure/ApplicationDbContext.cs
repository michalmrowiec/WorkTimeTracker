using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<DailyWorkSchedule> DailyWorkSchedules { get; set; }
        public DbSet<ActionTime> ActionTimes { get; set; }
        public DbSet<WorkActionTime> WorkActionTimes { get; set; }
        public DbSet<BreakActionTime> BreakActionTimes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentManager> DepartmentManagers { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DepartmentManager>()
                .HasKey(dm => new { dm.DepartmentId, dm.ManagerId });
        }
    }
}
