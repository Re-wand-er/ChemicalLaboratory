namespace ChemicalLaboratory.Models.Experiment
{
    public class ExperimentHistoryDataModel
    {
        public int idHistory { get; set; }           = 0;
        public  ExperimentDataModel idExperimentDataModel { get; set; } = new ExperimentDataModel();
        public string UserName { get; set; }         = string.Empty;
        public string OperationType { get; set; }    = string.Empty;
        public DateTime LastUpdateDate { get; set; } = DateTime.MinValue;

        public ExperimentHistoryDataModel() { } 
    }
}
