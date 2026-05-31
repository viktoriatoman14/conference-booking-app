using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConferenceBookingApp.Models;

namespace ConferenceBookingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext //to dziedzicznie automatycznie doda obsługe uzytkownikow
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ConferenceBookingApp.Models.Professors> Professors { get; set; }
        public DbSet<ConferenceRooms> ConferenceRooms { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
    }
}
