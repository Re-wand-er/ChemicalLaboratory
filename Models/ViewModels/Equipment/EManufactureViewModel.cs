using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.ViewModels
{
    public class EManufactureViewModel
    {
        public int IdManufacturer { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}