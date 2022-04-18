using com.minsoehanwin.sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF.EntityMap
{
    public class StoreProductConfiguration : EntityTypeConfiguration<StoreProduct>
    {
        public StoreProductConfiguration()
        {
            this.ToTable("StoreProduct");
            this.HasKey(x => x.StoreId)
                .HasKey(x => x.ProductId);
        }
    }
    
}
