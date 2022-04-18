using System;
using System.Collections.Generic;
namespace com.minsoehanwin.sample.Core.Models
{
    public class Employee : BaseEntityClass
    {
        private IList<Wife> _wifes;
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual int? Zipcode { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }

        public virtual int? MyStoreId { get; set; }//http://www.entityframeworktutorial.net/code-first/configure-one-to-one-relationship-in-code-first.aspx
        public virtual Store Store { get; set; }
        
        public virtual PassportInfo PassportInfo { get; set; }
        
        public virtual IList<Wife> Wifes
        {
            get
            {
                if (_wifes == null)
                {
                    _wifes = new List<Wife>();
                }
                return _wifes;
            }
            set
            {
                _wifes = value;
            }
        }

        public virtual Car Car { get; set; }
    }
}