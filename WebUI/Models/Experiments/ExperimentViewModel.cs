using Domain.DTOs;

namespace ChemicalLaboratory.WebUI.Models.Experiments
{
    public class ExperimentViewModel
    {
        public int idExperiment { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Result { get; set; }
        public string? Status { get; set; }

        //public ExperimentViewModel(ExperimentDTO experimentDTO)
        //{
        //    idExperiment = experimentDTO.idExperiment;
        //    Name = experimentDTO.Name;
        //    Description = experimentDTO.Description;
        //    StartDate = experimentDTO.StartDate;
        //    EndDate = experimentDTO.EndDate;
        //    Result = experimentDTO.Result;
        //    Status = experimentDTO.Status;
        //}
    }
}
