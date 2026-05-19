using ApiProjectPractise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiProjectPractise.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdatedAt)
                .IsRequired(false);

            // Seed Data for Categories
            builder.HasData(
    new Category
    {
        Id = 1,
        Name = "Phones",
        Description = "Mobile Phones Collection",
        ImageUrl = "phones.jpg",
        CreatedAt = new DateTime(2024, 1, 1)
    },
    new Category
    {
        Id = 2,
        Name = "Laptops",
        Description = "Laptops Collection",
        ImageUrl = "laptops.jpg",
        CreatedAt = new DateTime(2024, 1, 1)
    },
    new Category
    {
        Id = 3,
        Name = "Accessories",
        Description = "Tech Accessories",
        ImageUrl = "accessories.jpg",
        CreatedAt = new DateTime(2024, 1, 1)
    }
);
        }
    }
}
