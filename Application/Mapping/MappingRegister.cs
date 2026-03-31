using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Application.UseCases.DTOs.UserDTOs;
using ChemicalLaboratory.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Mapster;

namespace ChemicalLaboratory.Application.Mapping
{
    public class MappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config) 
        {
            // Notifications ----------------------------------------
            config.NewConfig<NotificationDTO, Notification>()
                .Ignore(n => n.Id)
                .Ignore(n => n.CreatedAt);

            config.NewConfig<Notification, NotificationDTO>();

            // Reagents ---------------------------------------------
            config.NewConfig<ReagentDTO, Reagent>()
                .Ignore(n => n.Id)
                .Ignore(n => n.CreatedAt);

            config.NewConfig<Reagent, ReagentDTO>();

            // Users ------------------------------------------------
            config.NewConfig<User, UserReadDTO>();
            config.NewConfig<UserReadDTO, User>();

            config.NewConfig<UserUpdateDTO, User>()
                .Ignore(d => d.PasswordHash ?? "" );

            // ?????
            var hasher = new PasswordHasher<User>();
            config.NewConfig<UserCreateDTO, User>()
                .Ignore(d => d.Id)
                //.Ignore(d => d.PasswordHash)
                //.Map(d => d.PasswordHash, s => hasher.HashPassword(null!, s.PasswordHash))
                .Map(d => d.IsActive, _ => true);

            // Suppliers --------------------------------------------
            config.NewConfig<SupplierDTO, Supplier>()
                .Ignore(s => s.Id);

            config.NewConfig<Supplier, SupplierDTO>();
        }
    }
}
