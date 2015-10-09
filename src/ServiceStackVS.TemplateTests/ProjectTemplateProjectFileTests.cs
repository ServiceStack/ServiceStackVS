using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace ServiceStackVS.Tests
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

        /// <summary>
        /// This test is for ensuring template project files aren't trying to include files that don't exist.
        /// If the plugin is packaged with anything missing, template will fail all together.
        /// </summary>
        [Test]
        public void IncludedFilesInProjectExistOnFileSystem()
        {
            string projectTemplatePath = Path.GetFullPath("..\\..\\..\\ServiceStackVS\\ProjectTemplates");
            var projectTemplateInfo = new DirectoryInfo(projectTemplatePath);
            var cSharpProjectFiles =
                projectTemplateInfo.GetFiles("*.csproj", SearchOption.AllDirectories)
                    .Where(x => !excludedProjectFiles.Contains(x.Name));
            var fSharpProjectFiles = projectTemplateInfo.GetFiles("*.fsproj", SearchOption.AllDirectories)
                    .Where(x => !excludedProjectFiles.Contains(x.Name));
            foreach (FileInfo cSharpProjectFile in cSharpProjectFiles)
            {
                var doc = XDocument.Load(cSharpProjectFile.FullName);
                var contentElements =
                    doc.Descendants()
                        .Where(x => 
                            (x.Name.LocalName == "Content" || x.Name.LocalName == "Compile" || x.Name.LocalName == "None") && 
                            x.HasAttributes && x.Attribute("Include") != null);
                foreach (var element in contentElements)
                {
                    AssertProjectHasFile(cSharpProjectFile,element);
                }
            }

            foreach (FileInfo cSharpProjectFile in fSharpProjectFiles)
            {
                var doc = XDocument.Load(cSharpProjectFile.FullName);
                var contentElements =
                    doc.Descendants()
                        .Where(x =>
                            (x.Name.LocalName == "Content" || x.Name.LocalName == "Compile" || x.Name.LocalName == "None") &&
                            x.HasAttributes && x.Attribute("Include") != null);
                foreach (var element in contentElements)
                {
                    AssertProjectHasFile(cSharpProjectFile, element);
                }
            }
        }

        private void AssertProjectHasFile(FileInfo rootPath, XElement contentElement)
        {
            string fullPath = Path.Combine(rootPath.Directory.FullName, contentElement.Attribute("Include").Value);
            if (!File.Exists(fullPath))
            {
                Console.WriteLine(rootPath.FullName);
                Console.WriteLine(fullPath);
            }
            Assert.That(fullPath,Is.Not.Null);
            Assert.That(File.Exists(fullPath),Is.True);
        }
    }
}
