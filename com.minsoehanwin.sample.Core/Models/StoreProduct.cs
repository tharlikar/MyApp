using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Core.Models
{
    public class StoreProduct : AuditableEntity
    {
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public virtual string Name { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as StoreProduct;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.ProductId == other.ProductId && this.StoreId == other.StoreId) return true;

            return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ ProductId.GetHashCode();
                hash = (hash * 31) ^ StoreId.GetHashCode();

                return hash;
            }
        }
    }
}
