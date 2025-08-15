namespace ChemicalLaboratory.Models.Equipment
{
    public class EquipmentDataModel
    {
        public int idEquipment { get; set; }    = 0;
        public string? Name { get; set; }        = "";
        public string? Model { get; set; }       = "";
        public string? Description { get; set; } = "";
        public string? Kind { get; set; }        = "";
        public string? Status { get; set; }      = "";

        public EquipmentDataModel() { }
    }
}
