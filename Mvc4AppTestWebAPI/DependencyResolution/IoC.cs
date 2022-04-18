// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
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


using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Services;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;
namespace Mvc4AppTestWebAPI.DependencyResolution
{
    //see link for StructureMap.MVC4 package;it will auto create code file in App_Start and in DependencyResolution folder
    //http://stackoverflow.com/questions/19476856/how-to-configure-structuremap-for-asp-net-mvc-5
    public static class IoC
    {
        public static IContainer Initialize()
        {
            Registry registry = new com.minsoehanwin.sample.Helper.MyRegistry();
            return Initialize(registry);
        }

        public static IContainer Initialize(Registry registry)
        {
            return new Container(registry);
        }
    }
}