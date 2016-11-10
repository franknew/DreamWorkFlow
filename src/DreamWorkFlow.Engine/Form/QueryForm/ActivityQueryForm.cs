using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class ActivityQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public string ActivityDefinitionID { get; set; }
        
        public List<String> ActivityDefinitionIDs { get; set;}
        public string Page { get; set; }
        
        public List<String> Creators { get; set;}
        public Int32? Type { get; set; }
        
        public string WorkflowID { get; set; }
        
        public List<String> WorkflowIDs { get; set;}
        public Int32? Status { get; set; }
        
        public DateTime? ReadTime_Start { get; set; }
        
        public DateTime? ReadTime_End { get; set; }
        
        public DateTime? ProcessTime_Start { get; set; }
        
        public DateTime? ProcessTime_End { get; set; }
        
        public string Title { get; set; }
        
    }
}
