using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.EmailEntity
{
    public class EmailAttachment
    {
        public EmailAttachment()
        {
        }
        public EmailAttachment(string fileName, string physicalFilePath)
        {
            FileName = fileName;
            PhysicalFilePath = physicalFilePath;
        }
        public virtual string EmailId { get; set; }
        public virtual Email Email { get; set; }
        public virtual string PhysicalFilePath { get; set; }
        public virtual string FileName { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as EmailAttachment;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.EmailId == other.EmailId &&
                this.FileName == other.FileName;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ EmailId.GetHashCode();
                hash = (hash * 31) ^ FileName.GetHashCode();
                return hash;
            }
        }
    }
}
