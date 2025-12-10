namespace Domain.Entities
{
    public class ExperimentEquipment
    {
        public int idExpEq { get; set; }
        public int? idExperiment { get; set; }
        public int? idEquipment { get; set; }

        public Equipment? Equipment { get; set; }
        public Experiment? Experiment { get; set; }
    }
}
