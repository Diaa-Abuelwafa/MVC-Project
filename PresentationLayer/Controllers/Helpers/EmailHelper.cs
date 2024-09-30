using PresentationLayer.Models.ViewModels;
using System.Net;
using System.Net.Mail;

namespace PresentationLayer.Controllers.Helpers
{
    public class EmailHelper
    {
        public void SendEmail(EmailViewModel Email)
        {
            // From Google Mail Server
            var Client = new SmtpClient("smtp.gmail.com", 587);

            // Configurations For Configure The 'From'
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("diaaabuelwafa@gmail.com", "Akmn123123");

            // Send This Mail
            Client.Send("diaaabuelwafa@gmail.com", Email.To, Email.Subject, Email.Body);
        }
    }
}
