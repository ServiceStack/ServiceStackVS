using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStackVS.Wizards;

namespace ServiceStackVS_UnitTests
{
    [TestClass]
    public class NpmTests
    {
        
        [TestMethod]
        public void Test_Invalid_Characters_On_Path()
        {
            //Test with invalid characters on path environment variable.
            var currentEnvironmentVariables = Environment.GetEnvironmentVariable("PATH");
            //Double quotes are invalid
            Environment.SetEnvironmentVariable("PATH", currentEnvironmentVariables + ";\"C:\\Test\"");
            var test = CommandUtils.GetFullPathToCommand("acommandthatdoesntexist");
            //Command not found but invalid characters ignored.
            Assert.IsNull(test);
        }
    }
}
