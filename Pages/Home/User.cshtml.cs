using ChemicalLaboratory.Models.People;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ChemicalLaboratory.Pages.Authorised
{
    [Authorize]
    public class UserModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public UserProfile? UserProfile { get; set; }

        public IActionResult OnGet()
        {
            int myId = 0;

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int Id);
                myId = Id;
            }
            //myId = JsonRequest.Instance(0).id;
            UserProfile = UserProfile.Instance(myId);

            return Page();
        }

        public async Task<IActionResult> OnPostLogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //HttpContext.Session.Clear();
            return RedirectToPage("/Home/Authorisation");
        }

        private string GetCookie(string key)
        {
            if (HttpContext.Request.Cookies.TryGetValue(key, out var value))
            {
                return value; // Возвращает значение cookie, если оно существует
            }
            return string.Empty; // Возвращает null, если cookie не найден
        }
    }
}
