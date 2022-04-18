using com.minsoehanwin.sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF.EntityMap
{
    public class PassportInfoEntityConfiguration : EntityTypeConfiguration<PassportInfo>
    {
        public PassportInfoEntityConfiguration()
        {
            //for  one(Employee) to zero or one(PassportInfo) relationshiop see http://www.entityframeworktutorial.net/code-first/configure-one-to-one-relationship-in-code-first.aspx
            //Employee can have one or zero PassportInfo: Meaning=>PassportInfo cannot exist without Employee
            // Configure PassportInfoId as PK for PassportInfo
            this.ToTable("PassportInfo", "dbo");
            this.HasKey(pi => pi.EmployeeId);
        }
    }
}
