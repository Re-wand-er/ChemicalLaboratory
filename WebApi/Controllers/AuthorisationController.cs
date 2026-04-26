using ChemicalLaboratory.Application.UseCases.Services;
using Microsoft.AspNetCore.Mvc;
using ChemicalLaboratory.WebApi.Models;
namespace ChemicalLaboratory.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthorisationController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<AuthorisationController> _logger;

        public AuthorisationController(UserService userService, ILogger<AuthorisationController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthorisationDTO model)
        {
            _logger.LogInformation($"Find user with login = {model.Login}");

            var userData = await _userService.LoginAsync(model.Login, model.Password);
            if (userData == null) return Unauthorized();

            Response.Cookies.Append("jwtToken", userData.Token, GetCookieOptions(DateTime.UtcNow.AddDays(1)));
            return Ok(new { user = userData.User });
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwtToken", GetCookieOptions());
            return Ok(new { message = "Logged out" });
        }


        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var user = await _userService.GetByIdAsync();
            return Ok(new { user });
        }


        [HttpPost("email-access")]
        public async Task<IActionResult> SendCode([FromBody] EmailRequestDTO model)
        {
            await _userService.SendResetCodeAsync(model.Email);
            return Ok();
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ChangePasswordDTO model)
        {
            var success = await _userService.ResetPasswordAsync(model.Email, model.Code, model.Password);
            return success ? Ok() : BadRequest(new { message = "Код неверен или просрочен" });
        }

        private CookieOptions GetCookieOptions(DateTime? expires = null) => new()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = expires,
            Path = "/"
        };
    }
}