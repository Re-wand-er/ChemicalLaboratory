using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Application.Interfaces
{
    public interface IUnitOfWork
    {
        INotificationRepository Notifications { get; }
        IReagentRepository Reagents { get; }
        IReagentOperationRepository ReagentOperations { get; }
        ISupplierRepository Suppliers { get; }
        IUserRepository Users { get; }

        Task SaveAsync();
    }
}
