using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Models
{
    
    public class Store : BaseEntityClass
    {
        
        protected IList<Product> _products;
        protected IList<Employee> _staffs;

        public virtual string Name { get; set; }
        public virtual IList<Product> Products
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
        public virtual IList<Employee> Staffs
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
