using ChemicalLaboratory.Application.UseCases.DTOs.UserDTOs;
using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Domain.Entities;
using ChemicalLaboratory.Domain.DTOs;
using Mapster;

// По хорошему должен быть отдельный mapster
using ChemicalLaboratory.WebApi.Models;

namespace ChemicalLaboratory.Application.Mapping
{
    public class MappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config) 
        {
            // Notifications ----------------------------------------
            config.NewConfig<Notification, NotificationDTO>();
            config.NewConfig<NotificationDTO, Notification>()
                .Ignore(n => n.Id)
                .Ignore(n => n.CreatedAt);

            // Reagents ---------------------------------------------
            config.NewConfig<Reagent, ReagentDTO>();
            config.NewConfig<ReagentDTO, Reagent>()
                .Ignore(n => n.Id)
                .Ignore(n => n.CreatedAt);


            config.NewConfig<ReagentDTO, ReagentCreateDTO>();
            config.NewConfig<ReagentCreateDTO, ReagentDTO>()
                .Ignore(r => r.Id)
                .Ignore(r => r.CreatedAt);


            config.NewConfig<ReagentCategory, CategoryDTO>();
            config.NewConfig<CategoryDTO, ReagentCategory>()
                .Ignore(r => r.Id);

            // Users ------------------------------------------------
            config.NewConfig<User, UserReadDTO>();
            config.NewConfig<UserReadDTO, User>();

            config.NewConfig<User, UserUpdateDTO>();
            config.NewConfig<UserUpdateDTO, User>()
                .Ignore(d => d.PasswordHash);

            config.NewConfig<UserCreateDTO, User>()
                .Ignore(d => d.Id)
                .Map(d => d.IsActive, _ => true);

            // Suppliers --------------------------------------------
            config.NewConfig<Supplier, SupplierDTO>();
            config.NewConfig<SupplierDTO, Supplier>()
                .Ignore(s => s.Id);

            config.NewConfig<SupplierDTO, SupplierWithoutIdDTO>();
            config.NewConfig<SupplierWithoutIdDTO, SupplierDTO>()
                .Ignore(r => r.Id);
        }
    }
}
