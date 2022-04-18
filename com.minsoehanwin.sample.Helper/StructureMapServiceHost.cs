using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace com.minsoehanwin.sample.Helper
{
    public class StructureMapServiceHost : ServiceHost
    {
        //https://lostechies.com/jimmybogard/2008/07/30/integrating-structuremap-with-wcf/
        //http://www.sgriffinusa.com/2011/02/setting-up-wcf-to-use-structuremap.html
        public StructureMapServiceHost()
        {
        }

        public StructureMapServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }

        protected override void OnOpening()
        {
            Description.Behaviors.Add(new StructureMapServiceBehavior());
            base.OnOpening();
        }
    }
}
