using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Entities
{
    public class BusinessBudyDbContext : DbContext
    {
        public BusinessBudyDbContext(DbContextOptions<BusinessBudyDbContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasOne(u => u.Address).WithOne(p => p.Company).HasForeignKey<Address>(a => a.CompanyId);
            modelBuilder.Entity<Client>().HasOne(u => u.Address).WithOne(p => p.Client).HasForeignKey<Address>(a => a.ClientId);
            modelBuilder.Entity<Order>().HasOne(u => u.Client).WithMany(p => p.Orders).HasForeignKey(a => a.ClientId);
            modelBuilder.Entity<Order>().HasOne(u => u.Invoice).WithOne();
            modelBuilder.Entity<OrderDetail>().HasOne(u => u.Order).WithOne(p => p.OrderDetail).HasForeignKey<OrderDetail>(a => a.OrderId);
            modelBuilder.Entity<OrderDetail>().HasMany(u => u.OrderProducts).WithOne(p => p.OrderDetail).HasForeignKey(a => a.OrderDetailId);
            modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany(p => p.User).HasForeignKey(a => a.RoleId);
            modelBuilder.Entity<OrderDetail>().HasOne(p => p.DeliveryAddress).WithOne(p => p.OrderDetail).HasForeignKey<OrderDetail>(a => a.DeliveryId);
        }
    }

}
