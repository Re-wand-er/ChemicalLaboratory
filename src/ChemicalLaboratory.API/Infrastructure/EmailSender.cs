using ChemicalLaboratory.Application.Interfaces;
using System.Net;
using System.Net.Mail;

namespace ChemicalLaboratory.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        public EmailSender() { }
        public async Task SendMailToEmail(string email, string subject, string body)
        {
            await Task.Run(() =>
            {

                MailAddress from = new MailAddress("tekirinka42@gmail.com", "TeKiRinKa Corp.");// адрес отправителя и его отображаемое имя

                MailAddress to = new MailAddress(email);

                MailMessage mail = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body
                };// Объект письма

                mail.IsBodyHtml = true;

                SmtpClient smtp_Client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("tekirinka42@gmail.com", "cwdwemmybjytsgij"),
                    EnableSsl = true
                };

                smtp_Client.SendMailAsync(mail);
            });

        }
    }
}
