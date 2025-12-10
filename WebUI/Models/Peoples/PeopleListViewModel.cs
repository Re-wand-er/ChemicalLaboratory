namespace ChemicalLaboratory.WebUI.Models.Peoples
{
	public class PeopleListViewModel
	{
		public UserControlModel UserControl { get; set; } = new UserControlModel();
		public IEnumerable<PeopleViewModel> People { get; set; } = [];
	}
}
