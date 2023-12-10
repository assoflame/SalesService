using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesService.Entities.Models;

namespace DataAccess.Configuration
{
    internal class RolesConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData
                (
                    new Role { Id = 1, Name = "user" },
                    new Role { Id = 2, Name = "admin" }
                );
        }
    }
}
