using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStackVS.FileHandlers;
using ServiceStackVS;

namespace ServiceStackVS_UnitTests
{
    [TestClass]
    public class DocumentExtensionTests
    {
        [TestMethod]
        public void Get_typehandler_from_file_list()
        {
            Dictionary<string, Type> selectedItems = new Dictionary<string, Type>
            {
                {"foo.dto.fs",typeof (FSharpNativeTypesHandler)},
                {"foo.dtos.fs",typeof (FSharpNativeTypesHandler)},
                {"foo.dtos.cs",typeof (CSharpNativeTypesHandler)},
                {"foo.dtos.vb",typeof (VbNetNativeTypesHandler)},
                {"foo.dtos.d.ts",typeof (TypeScriptNativeTypesHandler)},
            };

            foreach (var selectedItem in selectedItems)
            {
                var listOfItems = new List<SelectedItem>();
                listOfItems.Add(new MockSelecedItem { Name = selectedItem.Key });
                var typeOfHandler = listOfItems.GetTypeHandlerForSelectedFiles().First().GetType();
                Assert.AreEqual(selectedItem.Value, typeOfHandler);
            }
        }
    }

    public class MockSelecedItem : SelectedItem
    {
        public string Name { get; set; }

        //This here due to interface
        public SelectedItems Collection { get; private set; }
        public DTE DTE { get; private set; }
        public Project Project { get; private set; }
        public ProjectItem ProjectItem { get; private set; }
        
        public short InfoCount { get; private set; }
        public object get_Info(short index)
        {
            throw new NotImplementedException();
        }
    }
}
