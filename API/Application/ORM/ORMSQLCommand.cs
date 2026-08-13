using ChemicalLaboratory.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Domain
{
    public class ORMSQLCommand : DbContext
    {
        //public static string ip = "localhost,1433";
        //public static string connectionString = "Server=" + ip + ";Database=ChemicalLaboratory;TrustServerCertificate=True;User Id=sa;Password=#Root42@oAsDf4;"; //User Id=Server #root42_

        public ORMSQLCommand(DbContextOptions<ORMSQLCommand> options)
           : base(options)
        { }

        public DbSet<ReagentViewModel> Reagents { get; set; }
        //public DbSet<SupplierViewModel> Suppliers { get; set; }
        //public DbSet<ManufacturerViewModel> Manufacturers { get; set; }
        public DbSet<ReagentSupplierViewModel> ReagentSuppliers { get; set; }
        public DbSet<PurityViewModel> Purities { get; set; }
        public DbSet<ReagentManufacturerViewModel> ReagentManufacturers { get; set; }

        public DbSet<ExperimentViewModel> Experiments { get; set; }
        public DbSet<ExperimentHistoryViewModel> ExperimentHistories { get; set; }

        public DbSet<EquipmentViewModel> Equipments { get; set; }
        public DbSet<EquipmentManufacturerViewModel> EquipmentManufacturers { get; set; }
        public DbSet<EManufactureViewModel> EquipmentManufacturerCompanies { get; set; }

        //public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<PeopleViewModel> People { get; set; }

        public DbSet<ReagentExperimentViewModel> ReagentExperiments { get; set; }
        public DbSet<ExperimentEquipmentViewModel> ExperimentEquipments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer(connectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ReagentViewModel>().ToTable("Reagent", schema: "ReagentSchema");

            // Если Equipment или другая сущность не имеет ключа:
            // modelBuilder.Entity<Equipment>().HasNoKey();
        }
    }
}
