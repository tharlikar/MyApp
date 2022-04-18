using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.minsoehanwin.sample.Wcf.Service
{
    
    [DataContract]
    public class Wife
    {
        [DataMember]
        public Nullable<DateTime> CreatedDate { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public Nullable<DateTime> UpdatedDate { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public Employee Employee { get; set; }
    }
}
