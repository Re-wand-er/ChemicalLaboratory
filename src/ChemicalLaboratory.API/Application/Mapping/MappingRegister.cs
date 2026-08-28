using ChemicalLaboratory.Application.UseCases.DTOs.UserDTOs;
using ChemicalLaboratory.Application.UseCases.DTOs;
using ChemicalLaboratory.Domain.Entities;
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
            config.NewConfig<Notification, NotificationDTO>()
                .Map(dest => dest.ReagentName, src => src.Reagent != null ? src.Reagent.Name : null)
                .Map(dest => dest.UserName, src => src.User != null 
                    ? $"{src.User.LastName} {src.User.FirstName[0]}. {src.User.MiddleName[0]}."
                    : null);
            config.NewConfig<NotificationDTO, Notification>()
                .Ignore(n => n.Id)
                .Ignore(n => n.CreatedAt);

            // Reagents ---------------------------------------------
            config.NewConfig<Reagent, ReagentDTO>()
                .Map(dest => dest.CategoryName,  src => src.Category != null ? src.Category.Name : string.Empty);
            config.NewConfig<ReagentDTO, Reagent>()
                .Ignore(n => n.Id)
                .Ignore(n => n.CreatedAt);


            config.NewConfig<ReagentCreateDTO, Reagent>()
                .Ignore(r => r.Id)
                .Ignore(r => r.CreatedAt);

            config.NewConfig<ReagentUpdateDTO, Reagent>();
                //.Ignore(dest => dest.Category);

            config.NewConfig<ReagentCategory, CategoryDTO>();
            config.NewConfig<CategoryDTO, ReagentCategory>()
                .Ignore(r => r.Id);

            // Users ------------------------------------------------
            config.NewConfig<User, UserReadDTO>();
                // .Map(dest => dest.SystemRoleName, src => src.SystemRole != null ? src.SystemRole.Name : null)
                // .Map(dest => dest.WorkShift, src => src.WorkSchedule != null ? src.WorkSchedule.WorkShift : "")
                // .Map(dest => dest.StartTime, src => src.WorkSchedule != null ? src.WorkSchedule.StartTime.ToString(@"hh\:mm") : "")
                // .Map(dest => dest.EndTime, src => src.WorkSchedule != null ? src.WorkSchedule.EndTime.ToString(@"hh\:mm") : "");

            config.NewConfig<UserReadDTO, User>()
                .Ignore(r => r.SystemRole);



            config.NewConfig<User, UserUpdateDTO>();
            config.NewConfig<UserUpdateDTO, User>()
                .Ignore(d => d.PasswordHash);

            config.NewConfig<UserCreateDTO, User>()
                .Ignore(d => d.Id)
                .Map(d => d.IsActive, _ => true);

            // Suppliers --------------------------------------------
            config.NewConfig<Supplier, SupplierDTO>();
            config.NewConfig<SupplierDTO, Supplier>();
                //.Ignore(s => s.Id);

            config.NewConfig<Supplier, SupplierWithoutIdDTO>();
            config.NewConfig<SupplierWithoutIdDTO, Supplier>()
                .Ignore(r => r.Id)
                .Map(d => d.IsActive, _ => true);
        }
    }
}
