﻿using DataAccess.Configuration;
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
                .HasOne<User>(c => c.SecondUser)
                .WithMany(u => u.SecondChats)
                .HasForeignKey(c => c.SecondUserId);

            modelBuilder.Entity<Chat>()
                .HasOne<User>(c => c.FirstUser)
                .WithMany(u => u.FirstChats)
                .HasForeignKey(c => c.FirstUserId);

            modelBuilder.Entity<UserRating>()
                .HasOne<User>(ur => ur.User)
                .WithMany(u => u.RatingsAsSeller)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRating>()
                .HasOne<User>(ur => ur.UserWhoRated)
                .WithMany(u => u.RatingsAsCustomer)
                .HasForeignKey(ur => ur.UserWhoRatedId);

            modelBuilder.Entity<UserRole>()
                .HasKey(userRole => new { userRole.UserId, userRole.RoleId });

            modelBuilder.Entity<UserRating>()
                .HasKey(userRating => new { userRating.UserWhoRatedId, userRating.UserId });

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
