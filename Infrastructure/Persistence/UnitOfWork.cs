using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Domain.Interfaces;

namespace ChemicalLaboratory.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public INotificationRepository Notifications { get; }
        public IReagentRepository Reagents { get; }
        public ISupplierRepository Suppliers { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(
            DataBaseContext context,
            INotificationRepository notificationRepository,
            IReagentRepository reagentRepository,
            ISupplierRepository supplierRepository,
            IUserRepository userRepository)
        {
            _context = context;
            Notifications = notificationRepository;
            Reagents = reagentRepository;
            Suppliers = supplierRepository;
            Users = userRepository;
        }

        public Task SaveAsync()
            => _context.SaveChangesAsync();
    }
}
