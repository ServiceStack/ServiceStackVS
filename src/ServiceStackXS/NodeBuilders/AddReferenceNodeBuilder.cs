using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoDevelop.Ide.Gui.Components;

namespace ServiceStackXS.NodeBuilders
{
    public class AddReferenceNodeBuilder : TypeNodeBuilder
    {
        public override string GetNodeName(ITreeNavigator thisNode, object dataObject)
        {
            return "AddServiceStackReferenceItem";
        }

        public override Type NodeDataType
        {
            get { return typeof (AddReferenceItem); }
        }
    }

    public class AddReferenceItem
    {
        
    }
}
