using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
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
                        PasswordHash = "93C7076CD06276AA3B40BABBD75787D9",
                        PasswordSalt = "adminadmin"

                        // password = "SUPERUSER"
                    },
                    new User()
                    {
                        Id = 2,
                        Email = "test@gmail.com",
                        FirstName = "test_user",
                        LastName = "test",
                        City = "Kazan",
                        Age = 20,
                        PasswordHash = "237B99991325536F9735CAED4B785E30",
                        PasswordSalt = "test_usertest"

                        // password = "test"
                    }
                );
        }
    }
}
