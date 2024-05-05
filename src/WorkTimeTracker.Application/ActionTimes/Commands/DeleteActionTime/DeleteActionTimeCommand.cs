using MediatR;

namespace WorkTimeTracker.Application.ActionTimes.Commands.DeleteActionTime
{
    public class DeleteActionTimeCommand : IRequest
    {
        public string Id { get; set; }
        public string? BackLink { get; set; }
    }
}
