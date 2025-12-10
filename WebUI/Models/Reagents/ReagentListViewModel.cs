namespace ChemicalLaboratory.WebUI.Models.Reagents
{
    public class ReagentListViewModel
    {
        public bool isReagentVisible { get; set; } = true;
        public bool isManufactureVisible { get; set; } = false;
        public bool isSupplierVisible { get; set; } = false;

        /// <summary>
        /// 1 - реагенты; 
        /// 2 - производство 
        /// 3 - поставщики 
        /// 4 - реагенты и производство 
        /// 5 - производство и поставщик
        /// 6 - реагенты и поставщик 
        /// 7 - реагенты, производство и поставщик (в общем все)
        /// </summary>
        public int ParamOfKind { get; set; } = 1;


        public UserControlModel UserControl { get; set; } = new UserControlModel();
        public IEnumerable<ReagentInfoViewModel> Reagents { get; set; } = [];
    }
}
