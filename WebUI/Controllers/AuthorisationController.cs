using ChemicalLaboratory.Domain.UserRepository;
using ChemicalLaboratory.Pages.Home;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ChemicalLaboratory.WebUI.Models;

namespace ChemicalLaboratory.WebUI.Controllers
{
    public class AuthorisationController : Controller
    {
        private readonly IUserService _userService;

        public AuthorisationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Authorisation", new AuthorisationModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthorisationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError(string.Empty, "Пароль не может быть пустым!");
                model.ErrorMessage = "Пароль не может быть пустым!";
                return View(model);
            }

            var user = _userService.ValidateUser(model.Login, model.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль.");
                model.ErrorMessage = "Неверный логин или пароль!";
                return View(model);
            }

            JsonRequest.InstanceFree();
            JsonRequest.Instance(user.IdPeople);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdPeople.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.SystemRole)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home"); // или куда нужно перенаправить
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCode(AuthorisationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            //JsonRequest.InstanceFree();
            //model.IdMarker = RandomIdMarker.IdMarker;
            //JsonRequest.Instance(model.IdMarker);

            string subject = "Код идентификации:";
            //string body = "<h2>Пожалуйста введите ваш код в окне приложения.</h2><h3>Ваш идентификационный номер: </h3>" + model.IdMarker;

            try
            {
                //await MailSender.SendMailToEmail(model.Email, subject, body);
            }
            catch (Exception)
            {
                model.ErrorMessage = "Пожалуйста, корректно введите поле для почты";
            }

            return View("Login", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(AuthorisationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            try
            {
                //if (int.Parse(model.Code) == JsonRequest.Instance(model.IdMarker).id)
                //{
                //    SQLCommand.UpdatePeoplePassword(model.NewPassword, model.Login, model.Email);
                //    return RedirectToAction("Login"); // Возврат к странице логина после смены пароля
                //}
                //else
                //{
                //    ViewData["Message"] = "Неправильный идентификатор";
                //}
            }
            catch (Exception)
            {
                ViewData["Message"] = "Ошибка при смене пароля";
            }

            return View("Login", model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}