using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces.Repositories
{
    public interface IActionTimeRepository
    {
        Task<IEnumerable<ActionTime>> GetAllActionTimes();
        Task CreateActionTimeAsync(ActionTime actionTime);
    }
}
