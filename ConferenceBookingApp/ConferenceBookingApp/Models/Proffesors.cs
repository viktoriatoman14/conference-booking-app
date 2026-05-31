using System.ComponentModel.DataAnnotations;

namespace ConferenceBookingApp.Models
{
    public class Professors
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł naukowy")]
        public string AcademicTitle { get; set; } 

        [Required]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Profesor")]
        public string FullName => $"{AcademicTitle} {FirstName} {LastName}";
    }
}
