using Blog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Concrete.EntityFramework.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(70);
            builder.Property(c => c.Description).HasMaxLength(200);
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.isActive).IsRequired();
            builder.Property(c => c.isDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(300);
            builder.ToTable("Categories");
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "ASP.NET",
                    Description = "ASP.NET Kategorisi",
                    isActive = true,
                    isDeleted = false,
                    CreatedByName = "InitialCreate",
                    ModifiedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Note = "İlk eklenen kategori"
                },
                   new Category
                   {
                       Id = 2,
                       Name = "ASP.NET Core",
                       Description = "ASP.NET Core Kategorisi",
                       isActive = true,
                       isDeleted = false,
                       CreatedByName = "InitialCreate",
                       ModifiedByName = "InitialCreate",
                       CreatedDate = DateTime.Now,
                       ModifiedDate = DateTime.Now,
                       Note = "İkinci eklenen kategori"
                   }
            );
        }
    }
}
