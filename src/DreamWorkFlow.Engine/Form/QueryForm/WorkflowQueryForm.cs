using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class WorkflowQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public string WorkflowDefinitionID { get; set; }
        
        public List<String> WorkflowDefinitionIDs { get; set;}
        public List<String> Creators { get; set;}
        public Int32? Status { get; set; }
        
        public string ProcessID { get; set; }
        
        public List<String> ProcessIDs { get; set;}
    }
}
