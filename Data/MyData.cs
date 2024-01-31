using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MyData : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public MyData(DbContextOptions<MyData> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                  .HasOne(x => x.Category)
                  .WithMany(x => x.Products)
                  .HasForeignKey(x => x.CategoryId);
            modelBuilder.Entity<Product>()
                .HasMany(x=>x.OrderItem)
                .WithOne(x=>x.Product)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderItem)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.User)
                .WithMany(x => x.Order)
                .HasForeignKey(x => x.UserId);
        }
    }
}
