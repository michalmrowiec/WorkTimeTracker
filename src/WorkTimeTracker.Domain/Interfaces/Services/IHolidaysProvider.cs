using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces.Services
{
    public interface IHolidaysProvider
    {
        Task FetchHolidaysAsync(int year);
        Task<IEnumerable<Holiday>> GetHolidaysAsync(int year);
    }
}
