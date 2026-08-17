using ChemicalLaboratory.Infrastructure.Persistence.Configurations;
using ChemicalLaboratory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChemicalLaboratory.Infrastructure.Persistence
{
    public class DataBaseContext : DbContext
    {
        // Reagents
        public DbSet<ReagentCategory> ReagentCategories { get; set; }
        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<ReagentOperation> ReagentOperations { get; set; }
        // Users
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<User> Users { get; set; }
        // Suppliers
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ReagentReceipt> ReagentReceipts { get; set; }
        // Notifications
        public DbSet<NotificationSetting> NotificationSettings { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Reagent
            modelBuilder.ApplyConfiguration(new ReagentConfiguration());
            modelBuilder.ApplyConfiguration(new ReagentCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OperationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReagentOperationConfiguration());
            // User
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WorkScheduleConfiguration());
            // Supplier
            modelBuilder.ApplyConfiguration(new ReagentReceiptConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            // Notification
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationSettingConfiguration());
        }

        public DataBaseContext() { Database.EnsureCreated(); }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
    }
}
