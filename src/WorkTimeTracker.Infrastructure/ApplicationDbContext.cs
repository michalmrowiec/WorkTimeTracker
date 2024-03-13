using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

    }
}
