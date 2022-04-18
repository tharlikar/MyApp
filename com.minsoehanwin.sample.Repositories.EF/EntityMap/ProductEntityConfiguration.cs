using com.minsoehanwin.sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF.EntityMap
{
    
    public class ProductEntityConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductEntityConfiguration()
        {
            //http://www.entityframeworktutorial.net/code-first/configure-many-to-many-relationship-in-code-first.aspx
            this.ToTable("Product", "dbo");
            this.HasKey(p => p.Id);

            //Product has many stores
            //Deleting product will delete all rows associated with this ProductId from StoreProduct table.
            //But it will not effect Store table.
            this.HasMany<Store>(p => p.StoresStockedIn)
                //store has many products
                //Deleting store will delete all rows associated with this StoreId from StoreProduct table
                //But it will not effect Product table.
                .WithMany(s => s.Products)
                .Map(ps =>
                {
                    //IMPORTANT
                    //LeftKey mean "Product.ID" will be stored in StoreProduct.ProductId
                    //If StoreId is left key it means "ProductID" will be stored in StoreProduct.StoreId.
                    ps.MapLeftKey("ProductId");//Product foreign key for StoreProduct table
                    ps.MapRightKey("StoreId");//Store foreignkey for StoreProduct table
                    ps.ToTable("StoreProduct");//many to many relationship table name for Store and Product table
                });

        }
    }
}
