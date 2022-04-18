using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.ServiceModel.Activation;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;

namespace com.minsoehanwin.sample.Helper
{

    public class StructureMapInstanceProvider : IInstanceProvider
    {
        //https://lostechies.com/jimmybogard/2008/07/30/integrating-structuremap-with-wcf/
        //http://www.sgriffinusa.com/2011/02/setting-up-wcf-to-use-structuremap.html
        private readonly Type _serviceType;
        private static IContainer _container;
        public StructureMapInstanceProvider(Type serviceType)
        {
            _serviceType = serviceType;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            if(_container==null)
            _container=new Container(new MyRegistry());
            return _container.GetInstance(_serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }
    }
}