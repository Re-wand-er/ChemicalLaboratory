using ChemicalLaboratory.Application.UseCases.DTOs;

namespace ChemicalLaboratory.WebApi.Models
{
    public record AuthResponse(UserReadDTO User, string Token);

}
