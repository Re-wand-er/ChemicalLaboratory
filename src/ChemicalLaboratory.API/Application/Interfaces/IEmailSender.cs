namespace ChemicalLaboratory.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendMailToEmail(string email, string subject, string body);
    }
}
