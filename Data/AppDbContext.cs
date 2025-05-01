using Microsoft.EntityFrameworkCore;
using SalonBooking.Models;

namespace SalonBooking.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<SmsMessage> SmsMessages { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkerServiceRelation> WorkerServices { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkerServiceRelation>(b =>
            {
                b.HasKey(ws => new { ws.WorkerId, ws.ServiceId });

                b.HasOne(ws => ws.Worker)
                 .WithMany(w => w.WorkerServices)
                 .HasForeignKey(ws => ws.WorkerId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(ws => ws.Service)
                 .WithMany(s => s.WorkerServices)
                 .HasForeignKey(ws => ws.ServiceId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // Set precision and scale for decimal properties
            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18, 2)");  // Change to a suitable precision and scale

            modelBuilder.Entity<SubscriptionPlan>()
                .Property(sp => sp.PricePerMonth)
                .HasColumnType("decimal(18, 2)");  // Change to a suitable precision and scale

            // Other relationships and configurations
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Business)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BusinessId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
