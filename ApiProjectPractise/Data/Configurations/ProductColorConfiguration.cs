using ApiProjectPractise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiProjectPractise.Data.Configurations
{
    public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.ColorId });

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductColors)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Color)
                .WithMany(x => x.ProductColors)
                .HasForeignKey(x => x.ColorId);

            builder.HasData(

                new ProductColor { ProductId = 1, ColorId = 1 },
                new ProductColor { ProductId = 1, ColorId = 2 },

                new ProductColor { ProductId = 2, ColorId = 1 },
                new ProductColor { ProductId = 2, ColorId = 3 },

                new ProductColor { ProductId = 3, ColorId = 2 },
                new ProductColor { ProductId = 3, ColorId = 3 },

                new ProductColor { ProductId = 4, ColorId = 1 },
                new ProductColor { ProductId = 4, ColorId = 2 },

                new ProductColor { ProductId = 5, ColorId = 1 },
                new ProductColor { ProductId = 5, ColorId = 3 },

                new ProductColor { ProductId = 6, ColorId = 2 },
                new ProductColor { ProductId = 6, ColorId = 3 },

                new ProductColor { ProductId = 7, ColorId = 1 },
                new ProductColor { ProductId = 7, ColorId = 2 },

                new ProductColor { ProductId = 8, ColorId = 1 },
                new ProductColor { ProductId = 8, ColorId = 3 },

                new ProductColor { ProductId = 9, ColorId = 2 },
                new ProductColor { ProductId = 9, ColorId = 3 }

            );
        }
    }
}