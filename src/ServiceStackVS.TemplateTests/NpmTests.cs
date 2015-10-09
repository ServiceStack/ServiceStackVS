using System;
using NUnit.Framework;
using ServiceStackVS.Wizards;

namespace ServiceStackVS.Tests
{
    [TestFixture]
    public class NpmTests
    {
        
        [Test]
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
