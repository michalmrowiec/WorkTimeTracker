﻿using MediatR;

namespace WorkTimeTracker.Application.ActionTimes.Commands.CreateActionTime
{
    public class CreateActionTimeCommand : IRequest
    {
        public string EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public TimeSpan? TimeOfAction { get; set; }
        public bool IsWork { get; set; }
        public string? BackLink { get; set; }
    }
}
