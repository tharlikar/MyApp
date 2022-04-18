using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.minsoehanwin.sample.Wcf.Service
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        public Nullable<DateTime> CreatedDate { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public Nullable<DateTime> UpdatedDate { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }
        
        protected IList<Wife> _wifes;
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Address1 { get; set; }
        [DataMember]
        public string Address2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public int? Zipcode { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public int? MyStoreId { get; set; }
        [DataMember]
        public Store Store { get; set; }
        [DataMember]
        public PassportInfo PassportInfo { get; set; }
        [DataMember]
        public IList<Wife> Wifes
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
        [DataMember]
        public Car Car { get; set; }
    }
}