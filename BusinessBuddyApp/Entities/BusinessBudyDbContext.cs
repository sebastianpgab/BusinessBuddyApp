﻿using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Entities
{
    public class BusinessBudyDbContext : DbContext
    {
        public BusinessBudyDbContext(DbContextOptions<BusinessBudyDbContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Clothe> Clothes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Mug> Mugs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Other> Others { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasOne(u => u.Address).WithOne(p => p.Client).HasForeignKey<Address>(a => a.ClientId);
            modelBuilder.Entity<Order>().HasOne(u => u.Client).WithMany(p => p.Orders).HasForeignKey(a => a.ClientId);

        }
    }

}
