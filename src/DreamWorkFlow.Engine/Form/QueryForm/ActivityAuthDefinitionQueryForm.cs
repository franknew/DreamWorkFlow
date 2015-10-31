using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class ActivityAuthDefinitionQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public List<String> Creators { get; set;}
        public string Type { get; set; }
        
        public string Value { get; set; }
        
        public string ActivityDefinitionID { get; set; }
        
        public string WorkflowDefinitionID { get; set; }
        
        public UInt64? IsDeleted { get; set; }
        
    }
}
