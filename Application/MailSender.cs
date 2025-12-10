using System.Net;
using System.Net.Mail;

namespace ChemicalLaboratory.Domain
{
    public class MailSender
    {
        public MailSender() { }
        public static async Task SendMailToEmail(string email, string subject, string body)//int id_marker
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

                /*await */
                smtp_Client.SendMailAsync(mail);
            });

        }
    }

    public class RandomIdMarker
    {
        private static int _idmarker = CreateRandIdMarker();
        public static int IdMarker
        {
            get => _idmarker;
            set { _idmarker = value; }
        }
        private static int CreateRandIdMarker()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999);
        }

    }
}
