using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Infrastructure.Repositories
{
    internal class ActionTimeRepository : IActionTimeRepository
    {
        private readonly ApplicationDbContext _context;

        public ActionTimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateActionTimeAsync(ActionTime actionTime)
        {
            await _context.ActionTimes.AddAsync(actionTime);

            await _context.SaveChangesAsync();

        }

        public async Task DeleteActionTime(ActionTime actionTime)
        {
            _context.ActionTimes.Remove(actionTime);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionTime> GetActionTimeById(string id)
        {
            return await _context.ActionTimes
                .Include(at => at.Employee)
                .FirstAsync(at => at.Id == id);
        }

        public async Task<IEnumerable<ActionTime>> GetAllActionTimes()
        {
            return await _context.ActionTimes.ToListAsync();
        }

        public async Task<IEnumerable<ActionTime>> GetAllIncompleteActionTimesForEmployee(string employeeId)
        {
            return await _context.ActionTimes
                .Where(at => at.EmployeeId == employeeId
                    && !at.End.HasValue)
                .OrderBy(at => at.Start)
                .ToListAsync();
        }

        public async Task UpdateActionTimeAsync(ActionTime actionTime)
        {
            _context.ActionTimes.Entry(actionTime).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
