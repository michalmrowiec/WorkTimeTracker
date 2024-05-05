using MediatR;

namespace WorkTimeTracker.Application.ActionTimes.Commands.UpdateActionTime
{
    public class UpdateActionTimeCommand : IRequest
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public TimeSpan? TimeOfAction { get; set; }
        public bool IsWork { get; set; }
        public string? BackLink { get; set; }
    }
}
