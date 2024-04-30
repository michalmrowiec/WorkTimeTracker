using WorkTimeTracker.Domain.Entities;

namespace WorkTimeTracker.Domain.Interfaces.Repositories
{
    public interface IActionTimeRepository
    {
        Task<IEnumerable<ActionTime>> GetAllActionTimes();
        Task<IEnumerable<ActionTime>> GetAllIncompleteActionTimesForEmployee(string employeeId);
        Task<ActionTime> GetActionTimeById(string id);
        Task CreateActionTimeAsync(ActionTime actionTime);
        Task UpdateActionTimeAsync(ActionTime actionTime);
    }
}
