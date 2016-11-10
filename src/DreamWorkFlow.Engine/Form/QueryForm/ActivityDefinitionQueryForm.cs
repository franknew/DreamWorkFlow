using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class ActivityDefinitionQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public string WorkflowDefinitionID { get; set; }
        
        public List<String> WorkflowDefinitionIDs { get; set;}
        public string Page { get; set; }
        
        public UInt64? Enabled { get; set; }
        
        public Int32? Type { get; set; }
        
        public List<String> Creators { get; set;}
        public string Remark { get; set; }
        
        public string Title { get; set; }
        
        public UInt64? IsDeleted { get; set; }
        
    }
}
