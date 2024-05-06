namespace WorkTimeTracker.MVC.Models
{
    public class CreateForEmployees
    {
        public DateTime ActionTime { get; set; }
        public bool IsWork { get; set; }
        public bool IsStart { get; set; }
        public string EmployeeId { get; set; }
    }
}
