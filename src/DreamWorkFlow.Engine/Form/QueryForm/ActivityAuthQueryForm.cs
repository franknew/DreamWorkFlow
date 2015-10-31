using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class ActivityAuthQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public List<String> Creators { get; set;}
        public string ActivityAuthDefinitionID { get; set; }
        
        public string Type { get; set; }
        
        public string Value { get; set; }
        
        public string ActivityID { get; set; }
        
        public string WorkflowID { get; set; }
        
        public List<String> WorkflowIDs { get; set;}
    }
}
