using System.Collections.Generic;
using NUnit.Framework;
using ServiceStackVS.NativeTypes.Handlers;

namespace ServiceStackVS.Tests
{
    [TestFixture]
    public class NativeTypesTests
    {
        [Test]
        public void CSharpNativeTypesGeneratesCorrectUrl()
        {
            var typeHandler = new CSharpNativeTypesHandler();
            Dictionary<string, string> validUrls = new Dictionary<string, string>
            {
                {"http://localhost:8080/","http://localhost:8080/types/csharp"},
                {"http://example.com","http://example.com/types/csharp"},
                {"https://localhost:81/apis","https://localhost:81/apis/types/csharp"},
                {"https://localhost","https://localhost/types/csharp"}
            };

            foreach (var validUrl in validUrls)
            {
                NUnit.Framework.Assert.AreEqual(validUrl.Value, typeHandler.GenerateUrl(validUrl.Key, null));
            }
        }

        [Test]
        public void FSharpNativeTypesGeneratesCorrectUrl()
        {
            var typeHandler = new FSharpNativeTypesHandler();
            Dictionary<string, string> validUrls = new Dictionary<string, string>
            {
                {"http://localhost:8080/","http://localhost:8080/types/fsharp"},
                {"http://example.com","http://example.com/types/fsharp"},
                {"https://localhost:81/apis","https://localhost:81/apis/types/fsharp"},
                {"https://localhost","https://localhost/types/fsharp"}
            };

            foreach (var validUrl in validUrls)
            {
                NUnit.Framework.Assert.AreEqual(validUrl.Value, typeHandler.GenerateUrl(validUrl.Key, null));
            }
        }

        [Test]
        public void VbNetNativeTypesGeneratesCorrectUrl()
        {
            var typeHandler = new VbNetNativeTypesHandler();
            Dictionary<string, string> validUrls = new Dictionary<string, string>
            {
                {"http://localhost:8080/","http://localhost:8080/types/vbnet"},
                {"http://example.com","http://example.com/types/vbnet"},
                {"https://localhost:81/apis","https://localhost:81/apis/types/vbnet"},
                {"https://localhost","https://localhost/types/vbnet"}
            };

            foreach (var validUrl in validUrls)
            {
                NUnit.Framework.Assert.AreEqual(validUrl.Value, typeHandler.GenerateUrl(validUrl.Key, null));
            }
        }

        [Test]
        public void TypeScriptDNativeTypesGeneratesCorrectUrl()
        {
            var typeHandler = new TypeScriptNativeTypesHandler();
            Dictionary<string, string> validUrls = new Dictionary<string, string>
            {
                {"http://localhost:8080/","http://localhost:8080/types/typescript.d"},
                {"http://example.com","http://example.com/types/typescript.d"},
                {"https://localhost:81/apis","https://localhost:81/apis/types/typescript.d"},
                {"https://localhost","https://localhost/types/typescript.d"}
            };

            foreach (var validUrl in validUrls)
            {
                NUnit.Framework.Assert.AreEqual(validUrl.Value, typeHandler.GenerateUrl(validUrl.Key, null));
            }
        }

        [Test]
        public void TestLanguageStatsName()
        {
            var typesHandler = new TypeScriptNativeTypesHandler();
            Assert.AreEqual("typescript.d",typesHandler.RelativeTypesUrl.Substring(6));
        }
    }
}
