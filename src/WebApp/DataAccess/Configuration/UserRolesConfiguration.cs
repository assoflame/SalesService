using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesService.Entities.Models;

namespace DataAccess.Configuration
{
    internal class UserRolesConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData
                (
                    new UserRole()
                    {
                        UserId = 1,
                        RoleId = 1
                    },
                    new UserRole()
                    {
                        UserId = 1,
                        RoleId = 2
                    },
                    new UserRole()
                    {
                        UserId = 2,
                        RoleId = 1
                    }
                    );
        }
    }
}
