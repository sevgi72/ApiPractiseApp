using ApiProjectPractise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiProjectPractise.Data.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            //seed data 
            builder.HasData(
                new Color { Id = 1, Name = "Red" },
                new Color { Id = 2, Name = "Green" },
                new Color { Id = 3, Name = "Blue" },
                new Color { Id = 4, Name = "Yellow" },
                new Color { Id = 5, Name = "Black" },
                new Color { Id = 6, Name = "White" },
                new Color { Id = 7, Name = "Purple" },
                new Color { Id = 8, Name = "Orange" },
                new Color { Id = 9, Name = "Pink" },
                new Color { Id = 10, Name = "Gray" },
                new Color { Id = 11, Name = "Brown" },
                new Color { Id = 12, Name = "Cyan" },
                new Color { Id = 13, Name = "Magenta" },
                new Color { Id = 14, Name = "Lime" },
                new Color { Id = 15, Name = "Maroon" },
                new Color { Id = 16, Name = "Navy" },
                new Color { Id = 17, Name = "Olive" },
                new Color { Id = 18, Name = "Teal" },
                new Color { Id = 19, Name = "Silver" },
                new Color { Id = 20, Name = "Gold" }

            );
        }
    }
}
