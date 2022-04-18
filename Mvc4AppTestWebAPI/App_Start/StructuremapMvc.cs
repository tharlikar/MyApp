// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructuremapMvc.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;
using System.Web.Mvc;
using StructureMap;
using Mvc4AppTestWebAPI.DependencyResolution;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Mvc4AppTestWebAPI.App_Start.StructuremapMvc), "Start")]

namespace Mvc4AppTestWebAPI.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            
            //http://stackoverflow.com/questions/3971250/log4net-configuration-from-assembly-attribute-does-not-load-configuration-file
            var _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
			
            IContainer container = IoC.Initialize();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }
    }
}