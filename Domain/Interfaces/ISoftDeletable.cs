namespace ChemicalLaboratory.Domain.Interfaces
{
    public interface ISoftDeletable : IEntity
    {
        bool IsActive { get; set; }
        DateTime? DeletedAt { get; set; }
        //Task SoftDeleteAsync(IEnumerable<int> ids);
        //Task RestoreAsync(IEnumerable<int> ids);
    }
}
