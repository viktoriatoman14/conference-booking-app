using System.ComponentModel.DataAnnotations;

namespace ConferenceBookingApp.Models
{
    public class ConferenceRooms
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Numer sali")]
        public string Nnumber { get; set; } = string.Empty;


        [Required]
        [Display(Name = "Piętro")]
        public string Floor { get; set; } = string.Empty;


        [Display(Name = "Czy dostępna?")]
        public bool IsAvailable { get; set; } = true;

        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();
    }
}
