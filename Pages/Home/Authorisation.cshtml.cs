using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChemicalLaboratory.Domain;
using ChemicalLaboratory.Domain.UserServices;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ChemicalLaboratory.Pages.Home;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace ChemicalLaboratory.Pages.Home
{
    public class AuthorisationModel : PageModel
    {
        [BindProperty]
        int IdMarker { get; set; }

        [BindProperty]
        public string Login { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = " ";
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string NewPassword { get; set; } = string.Empty;

        [BindProperty]
        public string Code { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        /*public PeopleDataModel People { get; set; } = new PeopleDataModel();*/

        private readonly IUserService _userService; // Создайте сервис для работы с пользователями
        
        public AuthorisationModel(IUserService userService)
        {
            _userService = userService;
        }
        
        public void OnGet(){}

		private bool IsValidPassword(string password)
		{
			var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$");
			return regex.IsMatch(password);
		}

		public async Task<IActionResult> OnPostAuthorisationCheck() //OnPostAsync
        {

            //if (!IsValidPassword(Password)) 
            //{ 
            //    ErrorMessage = "Па";
            //    return Page(); 
            //}

			if (Password == null) 
            { 
                ModelState.AddModelError(string.Empty, "Пароль не может быть пустым!");
                ErrorMessage = "Пароль не может быть пустым!"; return Page(); 
            }
			// Проверка учетных данных
			var user = _userService.ValidateUser(Login, Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль.");
                ErrorMessage = "Неверный логин или пароль!";

				return Page();
            }

            JsonRequest.InstanceFree();
			JsonRequest.Instance(user.IdPeople);


			// Создание Claims для пользователя
			var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdPeople.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.SystemRole) // Добавляем роль
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Установка cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            //TempData["CurrentUser"] = $"{user.IdPeople}";
            return RedirectToPage("/Authorised/User"); // Перенаправление на главную страницу
        }

        public async /*void*/Task<IActionResult> OnPostMessageCode()
        {
            JsonRequest.InstanceFree();
            IdMarker = RandomIdMarker.IdMarker;
            JsonRequest.Instance(IdMarker);
            string subject = "Код идентификации:";
            string body = "<h2>Пожалуйста введите ваш код в окне приложения.</h2><h3>Ваш идентификационный номер: </h3>" + IdMarker;
            try
            {

                await MailSender.SendMailToEmail(Email, subject, body);
            }
            catch
            {
                ErrorMessage = "Пожалуйста, корректно введите поле для почты";
            }
            return Page();
        }

        public IActionResult OnPostNewPassword() //OnPostAsync
        {
            try
            {
                if (int.Parse(Code) == JsonRequest.Instance(IdMarker).id)
                {
                    SQLCommand.UpdatePeoplePassword(NewPassword, Login, Email);
                }
            }
            catch
            {
                ViewData["Message"] = "Неправильный идентификатор";
            }
            return RedirectToPage();
        }

    }
}
