using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Domain.Enums;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace ChemicalLaboratory.Application.UseCases.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor) { _contextAccessor = contextAccessor; }

        public virtual int? UserId => int.TryParse(_contextAccessor.HttpContext?.User?.FindFirst("id")?.Value, out var id) ? id : null;
        public virtual SystemRoleEnum? Role 
        {
            get
            {
                var roleName = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

                // Маппим строку из токена ("Admin") в наш Enum
                if (Enum.TryParse<SystemRoleEnum>(roleName, out var role))
                    return role;

                return null;
            }
        }

        public int GetRequiredUserId()
        {
            return UserId ?? throw new UnauthorizedAccessException();
        }
    }
}
