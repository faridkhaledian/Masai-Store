using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore.Mapping;

namespace ShopManagement.Infrastructure.EFCore
{
    public class ShopContext:DbContext
    {
        public DbSet <Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<Slide> Slides { get; set; }

        public ShopContext( DbContextOptions<ShopContext> options ) : base(options)
        {
            
             
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProductCategoryMapping).Assembly; //Go and get the entire assembly that contains ProductCategoryMapping.
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            //Go to all the classes that are inside this assembly and automatically configure the models (such as classes that implement IEntityTypeConfiguration<T>), automatically detect and apply.

            base.OnModelCreating(modelBuilder);


            //"Whenever EF Core wants to build models, it will
            //automatically read and apply all the settings defined
            //for the entities in configuration classes like
            //ProductCategoryMapping."






        }



    }
}
