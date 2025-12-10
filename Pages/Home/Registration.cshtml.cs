using ChemicalLaboratory.Domain;
using ChemicalLaboratory.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ChemicalLaboratory.Pages.Home
{
    public class RegistrationModel : PageModel
    {
        int IdMarker = 0;

        [BindProperty]
        public PeopleDataModel People { get; set; } = new PeopleDataModel();

        [BindProperty]
        public string identificationCode { get; set; } = string.Empty;

        [BindProperty]
        public int workSchedule { get; set; } = 0;

        [BindProperty]
        public string ErrorMessage { get; set; } = string.Empty;

        private bool IsValidPassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$");
            return regex.IsMatch(password);
        }

        public async Task<IActionResult> OnPostRegistration()
        {

            if (identificationCode != JsonRequest.Instance(IdMarker).id.ToString()) { ErrorMessage = "Неправильно введен код идентификации!"; return Page(); }

            if (!IsValidPassword(People.Password))
            {
                ErrorMessage = "Пароль не подходит. \nДолжен быть один Главный, специальный, строчной символ и число";
                return Page();
            }

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                // insert Запрос 
                string insertQuery =
                    "INSERT INTO PeopleSchema.People ( idWorkSchedule, FirstName, MiddleName, LastName, email, Sex, SystemRole, JobPosition, Login, PasswordHash)\r\n" +
                    "VALUES ( @idWorkSchedule, @FirstName, @MiddleName, @LastName, @email, @Sex, 'Пользователь', @JobPosition, @Login, @PasswordHash)";

                try
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        /*if (ValueCheck == IdMarker)
                        {*/

                        command.Parameters.AddWithValue("@idWorkSchedule", workSchedule);
                        command.Parameters.AddWithValue("@FirstName", People.FirstName);
                        command.Parameters.AddWithValue("@MiddleName", People.MiddleName);
                        command.Parameters.AddWithValue("@LastName", People.LastName);
                        command.Parameters.AddWithValue("@email", People.Email);
                        command.Parameters.AddWithValue("@Sex", People.Sex);
                        command.Parameters.AddWithValue("@JobPosition", People.JobPosition);
                        command.Parameters.AddWithValue("@Login", People.Login);
                        command.Parameters.AddWithValue("@PasswordHash", People.Password./*GetHashCode().*/ToString());

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected > 0) return RedirectToPage("/Home/Authorisation");
                        else
                        {
                            ErrorMessage = "Ошибка при регистрации. Попробуйте снова.";
                        }
                        /* }
                         else
                         {
                             ErrorMessage = "Введите код, который был прислан вам на почту.";
                         }*/
                    }
                }
                catch (SqlException ex)
                {
                    ErrorMessage = "Пользователь с такой почтой уже существует!";
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostMessageCode()
        {
            JsonRequest.InstanceFree();
            IdMarker = RandomIdMarker.IdMarker;
            JsonRequest.Instance(IdMarker);

            string subject = "Код идентификации:";
            string body = "<h2>Пожалуйста введите ваш код в окне приложения.</h2><h3>Ваш идентификационный номер: </h3>" + IdMarker;

            await MailSender.SendMailToEmail(People.Email, subject, body);


            return Page();
        }

        public void OnGet()
        {
        }

        public RegistrationModel() { }
    }

}
