using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.EmailEntity
{
    public abstract class EmailReceipient
    {
        public EmailReceipient()
        { }
        public EmailReceipient(string name, string emailAddress)
        {
            Name=name;
            EmailAddress=emailAddress;
        }
        public virtual string EmailId { get; set; }
        public virtual Email Email { get; set; }
        public virtual string Name { get; set; }
        public virtual string EmailAddress { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as EmailReceipient;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.EmailId == other.EmailId &&
                this.EmailAddress == other.EmailAddress;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ EmailId.GetHashCode();
                hash = (hash * 31) ^ EmailAddress.GetHashCode();
                return hash;
            }
        }
    }
}
