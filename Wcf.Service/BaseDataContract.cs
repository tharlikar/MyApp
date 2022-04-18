using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace com.minsoehanwin.sample.Wcf.Service
{
    //////http://www.codeproject.com/Articles/783569/WCF-Contract-Inheritance-problem-Explained-simply
    ////https://msdn.microsoft.com/en-us/magazine/gg598929.aspx
    //[DataContract(IsReference = true)]
    //[KnownType(typeof(Employee))]
    //[KnownType(typeof(Wife))]
    //[KnownType(typeof(PassportInfo))]
    //[KnownType(typeof(Store))]
    //public class BaseDataContract
    //{
    //    public BaseDataContract()
    //    {
    //    }

    //    [DataMember]
    //    public Nullable<DateTime> CreatedDate { get; set; }
    //    [DataMember]
    //    public string CreatedBy { get; set; }
    //    [DataMember]
    //    public Nullable<DateTime> UpdatedDate { get; set; }
    //    [DataMember]
    //    public string UpdatedBy { get; set; }
    //}
}
