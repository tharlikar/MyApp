using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Models
{
    public class Product : BaseEntityClass
    {
        protected IList<Store> _storesStockedIn;

        public virtual string Name { get; set; }
        public virtual double Price { get; set; }
        public virtual IList<Store> StoresStockedIn
        {
            get
            {
                if (_storesStockedIn == null)
                {
                    _storesStockedIn = new List<Store>();
                }
                return _storesStockedIn;
            }
            set
            {
                _storesStockedIn = value;
            }
        }
    }
}
