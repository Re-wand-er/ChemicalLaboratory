using System.ComponentModel.DataAnnotations;

namespace ChemicalLaboratory.Models.ViewModels
{
    public class EquipmentViewModel
    {
        public int IdEquipment { get; set; }
        public string Name { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string? Description { get; set; }
        public string Kind { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}