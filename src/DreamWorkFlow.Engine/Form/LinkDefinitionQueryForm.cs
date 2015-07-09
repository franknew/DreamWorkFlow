using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Form
{
    public class LinkDefinitionQueryForm : BaseQueryForm
    {
        public string FromActivityDefinitionID { get; set; }
        
        public string ToActivityDefinitionID { get; set; }
        
        public string Remark { get; set; }
        
    }
}
