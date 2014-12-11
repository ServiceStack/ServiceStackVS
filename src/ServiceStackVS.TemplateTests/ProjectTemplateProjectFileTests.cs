using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace ServiceStackVS.TemplateTests
{
    [TestFixture]
    public class ProjectTemplateProjectFileTests
    {
        //Excluded as MVC adds and includes these files on restore
        private List<string> excludedProjectFiles = new List<string>
        {
            "ServiceStack.CSharp.Mvc.csproj",
            "MVC4.csproj"
        };
        [Test]
        public void IncludedFilesInProjectExistOnFileSystem()
        {
            string projectTemplatePath = Path.GetFullPath("..\\..\\..\\ServiceStackVS\\ProjectTemplates");
            var projectTemplateInfo = new DirectoryInfo(projectTemplatePath);
            var cSharpProjectFiles =
                projectTemplateInfo.GetFiles("*.csproj", SearchOption.AllDirectories)
                    .Where(x => !excludedProjectFiles.Contains(x.Name));
            foreach (FileInfo cSharpProjectFile in cSharpProjectFiles)
            {
                var doc = XDocument.Load(cSharpProjectFile.FullName);
                var contentElements =
                    doc.Descendants()
                        .Where(x => 
                            x.Name.LocalName == "Content" && 
                            x.HasAttributes && x.Attribute("Include") != null);
                foreach (var element in contentElements)
                {
                    AssertProjectHasFile(cSharpProjectFile.Directory.FullName,element);
                }
            }
        }

        private void AssertProjectHasFile(string rootPath, XElement contentElement)
        {
            string fullPath = Path.Combine(rootPath, contentElement.Attribute("Include").Value);
            Assert.That(fullPath,Is.Not.Null);
            Assert.That(File.Exists(fullPath),Is.True);
        }
    }
}
