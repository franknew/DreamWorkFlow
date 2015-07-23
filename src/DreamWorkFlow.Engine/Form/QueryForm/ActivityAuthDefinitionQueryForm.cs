using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class ActivityAuthDefinitionQueryForm : SimpleQueryForm
    {
        public string Type { get; set; }
        
        public string Value { get; set; }
        
        public string ActivityDefinitionID { get; set; }
        
        public string WorkflowDefinitionID { get; set; }
        
    }
}
