using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class LinkDefinitionQueryForm : SimpleQueryForm
    {
        public string FromActivityDefinitionID { get; set; }
        
        public string ToActivityDefinitionID { get; set; }
        
        public string Remark { get; set; }
        
        public string WorkflowDefinitionID { get; set; }
        
    }
}
