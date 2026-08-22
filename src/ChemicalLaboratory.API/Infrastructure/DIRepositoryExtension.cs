using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Domain.Interfaces;
using ChemicalLaboratory.Infrastructure.Persistence;
using ChemicalLaboratory.Infrastructure.Persistence.Repositories;

namespace ChemicalLaboratory.Infrastructure;

public static class DIRepositoryExtension
{
	public static IServiceCollection AddScopedRepository(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IReagentRepository, ReagentRepository>();
        services.AddScoped<IReagentCategoryRepository, ReagentCategoryRepository>();
        services.AddScoped<IReagentOperationRepository, ReagentOperationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
        services.AddScoped<ISystemRoleRepository, SystemRoleRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

		return services;
	}
}