using com.minsoehanwin.sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Repositories.EF.EntityMap
{
    
    public class EmployeeEntityConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeEntityConfiguration()
        {
            //http://www.entityframeworktutorial.net/code-first/configure-one-to-many-relationship-in-code-first.aspx
            this.ToTable("Employee", "dbo");
            
            this.HasKey<int>(e => e.Id);

            this.HasMany(e => e.Wifes)
                .WithOptional(w => w.Employee)
                .HasForeignKey(w => w.EmployeeId)
                .WillCascadeOnDelete();

            this.Property(p => p.FirstName)
                .HasColumnName("FirstName")
                .HasColumnOrder(2)
                .HasMaxLength(50);

            //need to be nullable type//http://stackoverflow.com/questions/16037907/ef-5-0-multiplicity-error-on-simple-mapping
            //Adding employee using newly created store will create row in both 
            //Employee and Store table.
            //Deleting employee will not delete row in Store table
            //Deleting store will not delete row in employee table 
            //but MyStoreId will become NULL in employee row
            //foreign key need to be nullable type for HasOptional() see int? Employee.MyStoreId
            this.HasOptional<Store>(e => e.Store)//employee has one store
                //Employee has MyStoreId foreign key. Employee many -> Store one relationship
                //Adding employee using newly created store will create row 
                //in both Employee and Store table.
                //Deleting employee will not delete row in Store table
                //BUT deleting Store will delete rows from Employee table. *IMPORTANT*
                //foreign key need to be not nullable type for HasRequired() 
                //see int Employee.MyStoreId
                //Required to have Store reference(MyStoreId) in Employee row
                //.HasRequired<Store>(e=>e.Store)//employee has one store
                 .WithMany(s => s.Staffs)//store has many employee
                //Foreign key column name in employee table is "MyStoreId"
                 .HasForeignKey(e => e.MyStoreId);
            //even using .HasOptional() if this method is set to true 
            //it will delete row from Employee table
            //when store is deleted.
            //.WillCascadeOnDelete(true);


            // Configure EmployeeId as FK for PassportInfo table
            // Mark PassportInfo is optional for Employee,
            //HasOptional method allow Employee to have one or zero rows in PassportInfo table
            //Delete row PassportInfo will not delete row in Employee table
            //To delete PassportInfo of employee, 
            //just use e.PassportInfo=null and it will auto delete row in PassportInfo table
            //TO make one to one relationship use .WithRequired for both
            this.HasOptional<PassportInfo>(e => e.PassportInfo)
                // Create inverse relationship,WithRequired method creates inverse relationship 
                //by making EmployeeId column as FK in PassportInfo table
                //Row in PassportInfo table is dependent on row in Employee table. 
                //Thus,deleting row in Employee table will also delete row in PassportInfo table
                .WithRequired(pi => pi.Employee)
                //Create MSSQL table FK constraint. will set FK "INSERT or UPDATE action" to CASCADE.
                //Nhibernate mapping does not create these setting.
                .WillCascadeOnDelete();

            //one(employee) -> one(car) PK -> FK relationship
            this.HasOptional<Car>(e => e.Car)
                .WithRequired(c => c.Employee)
                .WillCascadeOnDelete();
        }
    }
}
