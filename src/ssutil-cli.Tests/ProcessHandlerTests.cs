using System;
using System.IO;
using NUnit.Framework;

namespace ssutil_cli.Tests
{
    [TestFixture]
    public class ProcessHandlerTests
    {

        [TestFixtureTearDown]
        public void TearDown()
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
            foreach(var file in dir.GetFiles("ServiceStackReference.*"))
            {
                File.Delete(file.FullName);
            }
        }

        [Test]
        public void Can_Add_Valid_ServiceStack_Url_CSharp()
        {
            string[] args = new[] { "http://techstacks.io/" };
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            ProcessModeHandler.Process(utilCli.Options);
            Assert.That(File.Exists("ServiceStackReference.dtos.cs"));
        }

        [Test]
        public void Can_Update_Valid_CSharp_File()
        {
            string[] args = new[] { Environment.CurrentDirectory + Path.DirectorySeparatorChar + "ServiceStackReference.dtos.cs" };
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            ProcessModeHandler.Process(utilCli.Options);
        }

        [Test]
        public void Can_Add_Valid_ServiceStack_Url_Java()
        {
            string[] args = new[] { "http://techstacks.io/", "-lang", "Java" };
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            ProcessModeHandler.Process(utilCli.Options);
            Assert.That(File.Exists("ServiceStackReference.java"));
        }

        [Test]
        public void Can_Update_Valid_Java_File()
        {
            string[] args = new[] { Environment.CurrentDirectory + Path.DirectorySeparatorChar + "ServiceStackReference.java" };
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            ProcessModeHandler.Process(utilCli.Options);
        }

        [Test]
        public void Can_Add_Valid_ServiceStack_Url_Kotlin()
        {
            string[] args = new[] { "http://techstacks.io/", "-lang", "Kotlin" };
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            ProcessModeHandler.Process(utilCli.Options);
            Assert.That(File.Exists("ServiceStackReference.kt"));
        }


        [Test]
        public void Can_Update_Valid_Kotlin_File()
        {
            string[] args = new[] { Environment.CurrentDirectory + Path.DirectorySeparatorChar + "ServiceStackReference.kt" };
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            ProcessModeHandler.Process(utilCli.Options);
        }

        [Test]
        public void Can_Add_Valid_ServiceStack_Url_Swift()
        {
            string[] args = new[] { "http://techstacks.io/", "-lang", "Swift" };
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            ProcessModeHandler.Process(utilCli.Options);
            Assert.That(File.Exists("ServiceStackReference.dtos.swift"));
        }

        [Test]
        public void Can_Update_Valid_Swift_File()
        {
            string[] args = new[] { Environment.CurrentDirectory + Path.DirectorySeparatorChar + "ServiceStackReference.dtos.swift" };
            var utilCli = new UtilCliOptions();
            utilCli.DefaultOptionSet.Parse(args);
            ProcessModeHandler.Process(utilCli.Options);
        }

        [Test]
        public void Throw_For_Invalid_BaseUrl()
        {
            string[] args = new[] { Environment.CurrentDirectory + Path.DirectorySeparatorChar + "ServiceStackRefInvalidUrl.dtos.cs" };
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
