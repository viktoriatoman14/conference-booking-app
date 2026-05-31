using System.Threading.Tasks;

namespace ConferenceBookingApp.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
