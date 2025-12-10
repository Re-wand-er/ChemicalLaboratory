namespace Domain.Entities
{
    public class ReagentExperiment
    {
        public int idReagExpetiment { get; set; }
        public int? idExperiment { get; set; }
        public int? idReagent { get; set; }
        public int? UseCount { get; set; }
        public decimal? Mass { get; set; }

        public Reagent? Reagent { get; set; }
        public Experiment? Experiment { get; set; }
    }
}
