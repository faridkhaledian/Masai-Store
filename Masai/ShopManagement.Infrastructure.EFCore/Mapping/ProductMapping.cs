using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
  public  class ProductMapping:IEntityTypeConfiguration<Product>
    {

       
      public  void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();    
            builder.Property(x => x.Code).HasMaxLength(15).IsRequired();    
            builder.Property(x => x.ShortDescription).HasMaxLength(500).IsRequired();    
            builder.Property(x => x.Picture).HasMaxLength(1000);    
            builder.Property(x => x.PictureAlt).HasMaxLength(255);    
            builder.Property(x => x.PictureTitle).HasMaxLength(500);
            
            builder.Property(x => x.KeyWords).HasMaxLength(100).IsRequired();    
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();    
            builder.Property(x => x.Slug).HasMaxLength(500).IsRequired();    

            builder.HasOne(x => x.Category).WithMany(x=> x.products).HasForeignKey(x => x.CategoryId);

            builder.HasMany(x => x.ProductPictures).WithOne(x=> x.Product).HasForeignKey(x => x.ProductId);
        }
    }
}
