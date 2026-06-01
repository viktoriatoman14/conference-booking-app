using System.ComponentModel.DataAnnotations;

namespace ConferenceBookingApp.Models
{
    public class PageContent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; } = string.Empty;

        [Required]
        public string Value { get; set; } = string.Empty;
    }
}
