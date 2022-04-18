using com.minsoehanwin.sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF.EntityMap
{
    
    public class CarEntityConfiguration : EntityTypeConfiguration<Car>
    {
        public CarEntityConfiguration()
        {
            this.ToTable("Car");
            this.HasKey(c => c.Id);
        }
    }
}
