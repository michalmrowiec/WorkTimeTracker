using MediatR;

namespace WorkTimeTracker.Application.ActionTimes.Queries.GetActionTimeById
{
    public record GetActionTimeByIdQuery(string Id) : IRequest<ActionTimeDto>;
}
