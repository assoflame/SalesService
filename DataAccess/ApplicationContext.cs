using DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chat>()
                .HasOne<User>(c => c.Seller)
                .WithMany(u => u.ChatsAsSeller)
                .HasForeignKey(c => c.SellerId);

            modelBuilder.Entity<Chat>()
                .HasOne<User>(c => c.Customer)
                .WithMany(u => u.ChatsAsCustomer)
                .HasForeignKey(c => c.CustomerId);

            modelBuilder.Entity<UserRating>()
                .HasOne<User>(ur => ur.Seller)
                .WithMany(u => u.RatingsAsSeller)
                .HasForeignKey(ur => ur.SellerId);

            modelBuilder.Entity<UserRating>()
                .HasOne<User>(ur => ur.Customer)
                .WithMany(u => u.RatingsAsCustomer)
                .HasForeignKey(ur => ur.CustomerId);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.ApplyConfiguration(new RolesConfiguration());
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
