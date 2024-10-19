using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantManagementSystem.Models
{
    public class RestaurantContext : IdentityDbContext<ApplicationUser>
    {
        public RestaurantContext(DbContextOptions options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Reversation> Reversations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-I7PU4G3;Database=RestaurantManagementSystemA;Trusted_Connection=True;Encrypt=false");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(4, 2)"); // الدقة 18 والأرقام بعد الفاصلة 2
            modelBuilder.Entity<MenuItem>()
                .Property(o => o.Price)
                .HasColumnType("decimal(3, 2)");
        }

    }
}
