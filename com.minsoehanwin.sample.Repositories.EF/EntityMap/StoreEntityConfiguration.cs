using com.minsoehanwin.sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF.EntityMap
{
    public class StoreEntityConfiguration : EntityTypeConfiguration<Store>
    {
        public StoreEntityConfiguration()
        {
            //all relationship for Store are already defined in Product(store many->product many) 
            //and Employee(store many -> employee many) configuration.
            this.ToTable("Store", "dbo");
            this.HasKey(s => s.Id);
        }
    }
}
