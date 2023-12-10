using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesService.Entities.Models;

namespace DataAccess.Configuration
{
    internal class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
                (
                    new User()
                    {
                        Id = 1,
                        Email = "admin@gmail.com",
                        FirstName = "admin",
                        LastName = "admin",
                        City = "Kazan",
                        Age = 100,
                        PasswordHash = "2553888B95F3BDEE678CED8C0984B039",
                        PasswordSalt = "adminadmin"

                        // password = "Admin123&"
                    },
                    new User()
                    {
                        Id = 2,
                        Email = "test@gmail.com",
                        FirstName = "test",
                        LastName = "test",
                        City = "Kazan",
                        Age = 20,
                        PasswordHash = "2A5A8368EC11522D8EE933ECE2A2C9F3",
                        PasswordSalt = "testtest"

                        // password = "Test123&"
                    }
                );
        }
    }
}
