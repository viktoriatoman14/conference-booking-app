using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceBookingApp.Models
{
    public class Bookings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data rozpoczęcia")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Data zakończenia")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Cel spotkania")]
        public string MeetingPurpose { get; set; } = string.Empty;

        // ID z ConferenceRooms
        [Required]
        public int ConferenceRoomId { get; set; }

        [ForeignKey("ConferenceRoomId")]
        public ConferenceRooms? ConferenceRoom { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        // Klucz obcy do tabeli Profesorów
        [Display(Name = "Profesor")]
        public int ProfessorId { get; set; }

        //pobieranie pełnych danych profesora
        public Professors Professor { get; set; }
    }
}
