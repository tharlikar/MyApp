using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.minsoehanwin.sample.Wcf.Service
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public Nullable<DateTime> CreatedDate { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public Nullable<DateTime> UpdatedDate { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }

        protected IList<Store> _storesStockedIn;
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Price { get; set; }
        [DataMember]
        public IList<Store> StoresStockedIn
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
