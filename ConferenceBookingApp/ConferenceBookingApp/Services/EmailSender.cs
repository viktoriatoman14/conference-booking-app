using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace ConferenceBookingApp.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            // Ustawienia nadawcy 
            emailMessage.From.Add(new MailboxAddress("System Rezerwacji Sal", "viktoria142407@gmail.com"));

            // Odbiorca 
            emailMessage.To.Add(new MailboxAddress("", email));

            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("viktoria142407@gmail.com", "nuwm rujo eslt pszl");

                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
