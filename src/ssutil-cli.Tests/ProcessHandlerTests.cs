using System;
using System.IO;
using NUnit.Framework;

namespace ssutil_cli.Tests
{
    [TestFixture]
    public class ProcessHandlerTests
    {

        [TearDown]
        public void TearDown()
        {
            if (File.Exists("ServiceStackReference.dtos.cs"))
            {
                File.Delete("ServiceStackReference.dtos.cs");
            }
        }

        [Test]
        public void Can_Update_Valid_CSharp_File()
        {
            string[] args = new[] {"TestDtos"  + Path.DirectorySeparatorChar + "ServiceStackRef.dtos.cs"};
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            ProcessModeHandler.Process(utilCli.Options);
        }

        [Test]
        public void Can_Add_Valid_ServiceStack_Url()
        {
            string[] args = new[] { "http://techstacks.io/" };
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            ProcessModeHandler.Process(utilCli.Options);
            Assert.That(File.Exists("ServiceStackReference.dtos.cs"));
        }

        [Test]
        public void Throw_For_Invalid_BaseUrl()
        {
            string[] args = new[] { "TestDtos"  + Path.DirectorySeparatorChar + "ServiceStackRefInvalidUrl.dtos.cs" };
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            bool result = false;
            try
            {
                ProcessModeHandler.Process(utilCli.Options);
            }
            catch (Exception)
            {
                result = true;
            }

            Assert.That(result);
        }
    }
}
