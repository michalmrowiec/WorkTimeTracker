using System.ComponentModel.DataAnnotations;

namespace WorkTimeTracker.Models
{
    public class CreateEmployeeModel
    {
        [Required]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
    }
}
