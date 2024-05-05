using MediatR;

namespace WorkTimeTracker.Application.ActionTimes.Commands.CreateEndOfActionTime
{
    public class CreateEndOfActionTimeCommand : IRequest
    {
        public string EmployeeId { get; set; }
        public DateTime End { get; set; }
        public bool IsWork { get; set; }
    }
}
