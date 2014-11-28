using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStackVS.FileHandlers;
using ServiceStackVS.NativeTypes.Handlers;

namespace ServiceStackVS_UnitTests
{
    [TestClass]
    public class NativeTypeUrlTests
    {
        [TestMethod]
        public void CSharp_builds_url()
        {
            var typeHandler = new CSharpNativeTypesHandler();
            Dictionary<string, string> validUrls = new Dictionary<string,string>
            {
                {"http://localhost:8080/","http://localhost:8080/types/csharp"},
                {"http://example.com","http://example.com/types/csharp"},
                {"https://localhost:81/apis","https://localhost:81/apis/types/csharp"},
                {"https://localhost","https://localhost/types/csharp"}
            };

            foreach (var validUrl in validUrls)
            {
                Assert.AreEqual(validUrl.Value, typeHandler.GenerateUrl(validUrl.Key,null));
            }
        }

        [TestMethod]
        public void FSharp_builds_url()
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
                Assert.AreEqual(validUrl.Value, typeHandler.GenerateUrl(validUrl.Key, null));
            }
        }

        [TestMethod]
        public void VbNet_builds_url()
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
                Assert.AreEqual(validUrl.Value, typeHandler.GenerateUrl(validUrl.Key, null));
            }
        }

        [TestMethod]
        public void TypeScript_builds_url()
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
                Assert.AreEqual(validUrl.Value, typeHandler.GenerateUrl(validUrl.Key, null));
            }
        }
    }
}
