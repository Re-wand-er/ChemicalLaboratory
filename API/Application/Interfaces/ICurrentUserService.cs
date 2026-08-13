using ChemicalLaboratory.Domain.Enums;

namespace ChemicalLaboratory.Application.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        SystemRoleEnum? Role { get; }

        int GetRequiredUserId();
    }
}
