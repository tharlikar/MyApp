using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.minsoehanwin.sample.Wcf.Service
{

    [DataContract]
    public class Store
    {
        [DataMember]
        public Nullable<DateTime> CreatedDate { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public Nullable<DateTime> UpdatedDate { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }

        protected IList<Employee> _staffs;
        protected IList<Product> _products;
        [DataMember]
        public int Id {get;set;}
        
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public IList<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    _products = new List<Product>();
                }
                return _products;
            }
            set
            {
                _products = value;
            }
        }

        [DataMember]
        public IList<Employee> Staffs
        {
            get
            {
                if (_staffs == null)
                {
                    _staffs = new List<Employee>();
                }
                return _staffs;
            }
            set
            {
                _staffs = value;
            }
        }
    }
}