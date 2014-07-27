using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using ServiceStack.CSharp.AngularJS.ServiceInterface;

namespace $saferootprojectname$
{
    [TestFixture]
    public class UnitTests
    {
        private readonly ServiceStackHost appHost;

        public UnitTests()
        {
            appHost = new BasicAppHost(typeof(MyServices).Assembly)
            {
                ConfigureContainer = container =>
                {
                    //Add your IoC dependencies here
                }
            }
            .Init();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            appHost.Dispose();
        }

        [Test]
        public void TestMethod1()
        {
            var service = appHost.Container.Resolve<MyServices>();

            var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World"));
        }
    }
}
