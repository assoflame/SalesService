using DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using SalesService.Entities.Models;

namespace DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            //this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Chat>()
                .HasOne<User>(c => c.SecondUser)
                .WithMany(u => u.SecondChats)
                .HasForeignKey(c => c.SecondUserId);

            modelBuilder
                .Entity<Chat>()
                .HasOne<User>(c => c.FirstUser)
                .WithMany(u => u.FirstChats)
                .HasForeignKey(c => c.FirstUserId);

            modelBuilder
                .Entity<Review>()
                .HasOne<User>(ur => ur.User)
                .WithMany(u => u.ReviewsAsSeller)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder
                .Entity<Review>()
                .HasOne<User>(ur => ur.UserWhoRated)
                .WithMany(u => u.ReviewsAsCustomer)
                .HasForeignKey(ur => ur.UserWhoRatedId);

            modelBuilder
                .Entity<UserRole>()
                .HasKey(userRole => new { userRole.UserId, userRole.RoleId });

            modelBuilder
                .Entity<Review>()
                .HasKey(userReview => new { userReview.UserWhoRatedId, userReview.UserId });

            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
