namespace ChemicalLaboratory.WebUI.Models
{
    public class UserControlModel
    {
        public string? SearchQuery { get; set; }
        public int FilterCategory { get; set; }
        public int OrderBy { get; set; }
        public bool Ascending { get; set; } = true;
    }
}
