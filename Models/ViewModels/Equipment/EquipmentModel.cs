namespace ChemicalLaboratory.Models.ViewModels.Equipment
{
    public class EquipmentModel
    {
        public IEnumerable<EquipmentManufacturerViewModel> Model { get; set; } = [];
        //public UserControlModel UserControl { get; set; } = null!;
        public bool isEquipmentVisible { get; set; } = true;
        public bool isManufactureVisible { get; set; } = false;

        /// <summary>
        /// 1 - оборудование; 
        /// 2 - производство; 
        /// 3 - производство и обурадование
        /// </summary>
        public int ParamOfKind { get; set; } = 1;
    }
}
