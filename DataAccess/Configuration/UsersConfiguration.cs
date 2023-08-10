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
    internal class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
                (
                    new User()
                    {
                        Email = "admin@gmail.com",
                        FirstName = "admin",
                        LastName = "admin",
                        City = "Kazan",
                        Age = 100,
                        PasswordHash = "93C7076CD06276AA3B40BABBD75787D9",
                        PasswordSalt = "adminadmin"

                        // admin password = "SUPERUSER"
                    }
                );
        }
    }
}
