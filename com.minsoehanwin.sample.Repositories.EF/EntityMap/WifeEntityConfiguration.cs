using com.minsoehanwin.sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF.EntityMap
{
    public class WifeEntityConfiguration : EntityTypeConfiguration<Wife>
    {
        public WifeEntityConfiguration()
        {
            //one(employee) to many(wife) 
            this.ToTable("Wife");
            this.HasKey(w => w.Id);
            //cannot add unique constrained into foreign key EMployeeId in EF6 only available in EF7
            //modelBuilder.Entity<Wife>()
            //    .Property(w => w.EmployeeId)
            //    .HasColumnAnnotation(
            //                            "Index"
            //                            , new IndexAnnotation(
            //                                                    new[] { new IndexAttribute("Index") { IsUnique = true } }
            //                                                 )
            //                        );

        }
    }
}
