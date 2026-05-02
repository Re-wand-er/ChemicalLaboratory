using ChemicalLaboratory.Domain.Enums;
using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Domain.Entities
{
    public class ReagentOperation : IEntity
    {
        public int Id { get; set; }
        public int ReagentId { get; set; }
        public int UserId { get; set; }
        public int OperationTypeId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime OperationDate { get; init; }
        public string? Comment { get; set; }

        public Reagent Reagent { get; set; } = null!;
        public User User { get; set; } = null!;
        public OperationType OperationType { get; set; } = null!;

        private ReagentOperation() { }

        public static ReagentOperation Create(int userId, OperationTypeEnum operationType, Reagent reagent, string? comment = "") 
        {
            if (reagent == null) throw new ArgumentNullException(nameof(reagent));

            return new ReagentOperation
            {
                UserId = userId,
                OperationTypeId = (int)operationType,
                Quantity = reagent.CurrentQuantity,
                OperationDate = DateTime.UtcNow,
                Comment = string.IsNullOrWhiteSpace(comment)
                    ? $"Операция {operationType} над реагентом {reagent.Name}"
                    : comment,
                Reagent = reagent
            };
        }

        public static ReagentOperation CreateForDeletion(int userId, int reagentId, string comment)
        {
            return new ReagentOperation
            {
                UserId = userId,
                ReagentId = reagentId, 
                OperationTypeId = (int)OperationTypeEnum.WriteOff,
                Quantity = 0,
                OperationDate = DateTime.UtcNow,
                Comment = comment
            };
        }

        public static ReagentOperation CreateForUpdate(int userId, int reagentId, string comment)
        {
            return new ReagentOperation
            {
                UserId = userId,
                ReagentId = reagentId,
                OperationTypeId = (int)OperationTypeEnum.Update,
                Quantity = 0,
                OperationDate = DateTime.UtcNow,
                Comment = comment
            };
        }
    }
}
