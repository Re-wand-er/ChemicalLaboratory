namespace ChemicalLaboratory.Models.Equipment
{
    public class EManufacturerDataModel
    {
        public int idManufacturer { get; set; } = 0;
        public string? email { get; set; }       = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty ;
        public string? Address {  get; set; }    = string.Empty ; 
        public string? City { get; set; }        = string.Empty;
        public string? Country { get; set; }     = string.Empty;
        public EManufacturerDataModel() { }
    }
}
