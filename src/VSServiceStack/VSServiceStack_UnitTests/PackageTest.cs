/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.Collections;
using System.Text;
using System.Reflection;
using Microsoft.VsSDK.UnitTestLibrary;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStackVS;

namespace VSServiceStack_UnitTests
{
    [TestClass()]
    public class PackageTest
    {
        [TestMethod()]
        public void CreateInstance()
        {
            ServiceStackVSPackage package = new ServiceStackVSPackage();
        }

        [TestMethod()]
        public void IsIVsPackage()
        {
            ServiceStackVSPackage package = new ServiceStackVSPackage();
            Assert.IsNotNull(package as IVsPackage, "The object does not implement IVsPackage");
        }

    }
}
