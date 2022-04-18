using com.minsoehanwin.sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF
{
    public class MyCustomDBInitializer : DropCreateDatabaseAlways<MyDataContext>
    {
        protected override void Seed(MyDataContext context)
        {
            //context.Employees.Add(new Employee() { FirstName = "Min Soe Han", LastName = "Win", Address1 = "Added in DB Context initializer." });
            base.Seed(context);
        }
    }
}
