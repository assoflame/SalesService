using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesService.Entities.Models;

namespace DataAccess.Configuration
{
    internal class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
                (
                    new Product
                    {
                        Id = 1,
                        UserId = 1,
                        Name = "admin product",
                        Description = "admin product description",
                        Price = 1000,
                        IsSold = false,
                        CreationDate = DateTime.UtcNow
                    },
                    new Product
                    {
                        Id = 2,
                        UserId = 2,
                        Name = "product",
                        Description = "description",
                        Price = 1000,
                        IsSold = false,
                        CreationDate = DateTime.UtcNow
                    }
                );
        }
    }
}
