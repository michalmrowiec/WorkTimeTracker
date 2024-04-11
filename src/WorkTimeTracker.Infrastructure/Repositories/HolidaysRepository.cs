using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Domain.Interfaces.Repositories;

namespace WorkTimeTracker.Infrastructure.Repositories
{
    internal class HolidaysRepository : IHolidaysRepository
    {
        private readonly ApplicationDbContext _context;

        public HolidaysRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateHolidaysAsync(IEnumerable<Holiday> holidays)
        {
            await _context.Holidays.AddRangeAsync(holidays);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Holiday>> GetHolidaysAsync(int year)
        {
            var holidays = await _context.Holidays
                .Where(h => h.Date.Year == year)
                .AsNoTracking()
                .ToListAsync();

            return holidays;
        }
    }
}
