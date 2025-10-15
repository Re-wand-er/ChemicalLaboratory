using ChemicalLaboratory.Models.NewModels;
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

        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ReagentSupplier> ReagentSuppliers { get; set; }
        public DbSet<Purity> Purities { get; set; }
        public DbSet<ReagentManufacturer> ReagentManufacturers { get; set; }

        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<ExperimentHistory> ExperimentHistories { get; set; }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentManufacturer> EquipmentManufacturers { get; set; }
        public DbSet<EquipmentManufacturerCompany> EquipmentManufacturerCompanies { get; set; }

        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<People> People { get; set; }

        public DbSet<ReagentExperiment> ReagentExperiments { get; set; }
        public DbSet<ExperimentEquipment> ExperimentEquipments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseSqlServer(connectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reagent>().ToTable("Reagent", schema: "ReagentSchema");

            // Если Equipment или другая сущность не имеет ключа:
            // modelBuilder.Entity<Equipment>().HasNoKey();
        }
    }
}
