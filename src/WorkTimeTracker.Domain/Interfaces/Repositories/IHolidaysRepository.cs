using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces.Repositories
{
    public interface IHolidaysRepository
    {
        Task CreateHolidaysAsync(IEnumerable<Holiday> holidays);
        Task<IEnumerable<Holiday>> GetHolidaysAsync(int year);
    }
}
