using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.WebUI.Models
{
    public class AuthorisationModel
    {
        [Required(ErrorMessage = "Логин обязателен")]
        [Display(Name = "Логин")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Новый пароль обязателен")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Код обязателен")]
        [Display(Name = "Код подтверждения")]
        public string Code { get; set; } = string.Empty;

        public int IdMarker { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
