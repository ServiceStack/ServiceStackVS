using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace ssutil_cli.Tests
{
    [TestFixture]
    public class CliTests
    {
        [Test]
        public void Invalid_if_no_arguments()
        {
            //IE, this is there we would show help
            var result = ProcessModeHandler.GetMode(new Dictionary<string, string>());
            Assert.That(result, Is.EqualTo(CmdMode.Invalid));
        }

        [Test]
        public void GetMode_Throws_With_No_Args()
        {
            bool result = false;
            try
            {
                ProcessModeHandler.GetMode(null);
            }
            catch (Exception)
            {
                result = true;
            }

            Assert.That(result);
        }

        [Test]
        public void Add_Reference_Mode_Given_Just_Url()
        {
            var options = new Dictionary<string, string>()
            {
                {UtilCliOptions.DEFAULT,"http://techstacks.io/"}
            };
            var result = ProcessModeHandler.GetMode(options);
            Assert.That(result, Is.EqualTo(CmdMode.AddReference));
        }

        [Test]
        public void Add_Reference_Mode_Given_Url_And_Path()
        {
            var options = new Dictionary<string, string>()
            {
                {UtilCliOptions.DEFAULT,"http://techstacks.io/"},
                {UtilCliOptions.FILE,"myfile.dto.cs"}
            };
            var result = ProcessModeHandler.GetMode(options);
            Assert.That(result, Is.EqualTo(CmdMode.AddReferenceWithPath));
        }

        [Test]
        public void Add_Reference_Mode_Given_Url_And_Language()
        {
            var options = new Dictionary<string, string>()
            {
                {UtilCliOptions.DEFAULT,"http://techstacks.io/"},
                {UtilCliOptions.LANG,"FSharp"}
            };
            var result = ProcessModeHandler.GetMode(options);
            Assert.That(result, Is.EqualTo(CmdMode.AddRefWithLang));
        }

        [Test]
        public void Add_Reference_Mode_Given_Url_Path_And_Language()
        {
            var options = new Dictionary<string, string>()
            {
                {UtilCliOptions.DEFAULT,"http://techstacks.io/"},
                {UtilCliOptions.FILE,"myfile.dto.cs"},
                {UtilCliOptions.LANG,"FSharp"}
            };
            var result = ProcessModeHandler.GetMode(options);
            Assert.That(result, Is.EqualTo(CmdMode.AddReferenceWithPathAndLang));
        }

        [Test]
        public void Update_Mode_Given_Path()
        {
            var options = new Dictionary<string, string>()
            {
                {UtilCliOptions.DEFAULT,"TestDtos" + Path.DirectorySeparatorChar + "ServiceStackRef.dtos.cs"}
            };
            var result = ProcessModeHandler.GetMode(options);
            Assert.That(result, Is.EqualTo(CmdMode.UpdateReference));
        }

        [Test]
        public void Options_Exist_If_Only_Url()
        {
            string[] args = { "http://techstacks.io/" };
            var optionsUtil = new UtilCliOptions();
            optionsUtil.DefaultOptionSet.Parse(args);
            Assert.That(optionsUtil.Options[UtilCliOptions.DEFAULT], Is.EqualTo("http://techstacks.io/"));
        }

        [Test]
        public void Populate_Lang_Option_Correctly()
        {
            string[] args = {"-lang","CSharp"};
            var optionsUtil = new UtilCliOptions();
            optionsUtil.DefaultOptionSet.Parse(args);
            Assert.That(optionsUtil.Options[UtilCliOptions.LANG], Is.EqualTo("CSharp"));
        }

        [Test]
        public void Populate_Lang_Option_Correctly_DoubleDash()
        {
            string[] args = { "--lang", "CSharp" };
            var optionsUtil = new UtilCliOptions();
            optionsUtil.DefaultOptionSet.Parse(args);
            Assert.That(optionsUtil.Options[UtilCliOptions.LANG], Is.EqualTo("CSharp"));
        }

        [Test]
        public void Populate_UpdateRef_File_Option_Correctly()
        {
            string[] args = { "-file", "."  + Path.DirectorySeparatorChar + "TestDtos"  + Path.DirectorySeparatorChar + "ServiceReference.dto.cs" };
            var optionsUtil = new UtilCliOptions();
            optionsUtil.DefaultOptionSet.Parse(args);
            Assert.That(optionsUtil.Options[UtilCliOptions.FILE], Is.EqualTo("." + Path.DirectorySeparatorChar + "TestDtos" + Path.DirectorySeparatorChar + "ServiceReference.dto.cs"));
        }

        [Test]
        public void Populate_UpdateRef_File_Option_Correctly_DoubleDash()
        {
            string[] args = { "--file", "."  + Path.DirectorySeparatorChar + "TestDtos"  + Path.DirectorySeparatorChar + "ServiceReference.dto.cs" };
            var optionsUtil = new UtilCliOptions();
            optionsUtil.DefaultOptionSet.Parse(args);
            Assert.That(optionsUtil.Options[UtilCliOptions.FILE], Is.EqualTo("."  + Path.DirectorySeparatorChar + "TestDtos"  + Path.DirectorySeparatorChar + "ServiceReference.dto.cs"));
        }

        [Test]
        public void Update_Ref_Works_Without_URL()
        {
            string[] args = { "http://techstacks.io/", "--file", "."  + Path.DirectorySeparatorChar + "TestDtos"  + Path.DirectorySeparatorChar + "ServiceReference.dto.cs" };
            var optionsUtil = new UtilCliOptions();
            optionsUtil.DefaultOptionSet.Parse(args);
            Assert.That(optionsUtil.Options[UtilCliOptions.FILE], Is.EqualTo("."  + Path.DirectorySeparatorChar + "TestDtos"  + Path.DirectorySeparatorChar + "ServiceReference.dto.cs"));
            Assert.That(optionsUtil.Options[UtilCliOptions.DEFAULT], Is.EqualTo("http://techstacks.io/"));
        }
    }
}
