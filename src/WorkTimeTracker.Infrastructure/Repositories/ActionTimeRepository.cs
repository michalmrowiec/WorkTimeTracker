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

        public async Task<IEnumerable<ActionTime>> GetAllActionTimes()
        {
            return await _context.ActionTimes.ToListAsync();
        }
    }
}
