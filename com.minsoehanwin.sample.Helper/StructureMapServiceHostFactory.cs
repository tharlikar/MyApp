using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace com.minsoehanwin.sample.Helper
{
    public class StructureMapServiceHostFactory : ServiceHostFactory
    {
        //https://lostechies.com/jimmybogard/2008/07/30/integrating-structuremap-with-wcf/
        //http://www.sgriffinusa.com/2011/02/setting-up-wcf-to-use-structuremap.html
        public StructureMapServiceHostFactory()
        {
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new StructureMapServiceHost(serviceType, baseAddresses);
        }
    }
}
