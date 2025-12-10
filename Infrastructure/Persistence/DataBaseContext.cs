using Infrastructure.Persistence.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DataBaseContext : DbContext
    {
        //ReagentSchema
        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Purity> Purities { get; set; }
        public DbSet<ReagentManufacturer> ReagentManufacturers { get; set; }
        //PeopleSchema
        public DbSet<People> Peoples { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        //EquipmentSchema
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ManufacturerEquip> ManufacturerEquips { get; set; }
        public DbSet<EquipmentManufacturer> EquipmentManufacturers { get; set; }
        //ExperimentSchema
        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<History> Histories { get; set; }
        //dbo
        public DbSet<ReagentExperiment> ReagentExperiments { get; set; }
        public DbSet<ExperimentEquipment> ExperimentEquipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Purity здесь не подключается там одна аннотация [Key]
            modelBuilder.Entity<Purity>().ToTable("Purity", schema: "ReagentSchema");

            //ReagentSchema
            modelBuilder.ApplyConfiguration(new ReagentConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new ReagentManufacturerConfiguration());
            //PeopleSchema
            modelBuilder.ApplyConfiguration(new PeopleConfiguration());
            modelBuilder.ApplyConfiguration(new WorkScheduleConfiguration());
            //EquipmentSchema
            modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerEquipConfiguration());
            modelBuilder.ApplyConfiguration(new EquipmentManufacturerConfiguration());
            //ExperimentSchema
            modelBuilder.ApplyConfiguration(new ExperimentConfiguration());
            modelBuilder.ApplyConfiguration(new HistoryConfiguration());
            //dbo
            modelBuilder.ApplyConfiguration(new ReagentExperimentConfigurations());
            modelBuilder.ApplyConfiguration(new ExperimentEquipmentConfigurations());
        }

        public DataBaseContext() { Database.EnsureCreated(); }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
    }
}
