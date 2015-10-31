using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class ApprovalQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public string ActivityID { get; set; }
        
        public Int32? Status { get; set; }
        
        public string Remark { get; set; }
        
        public List<String> Creators { get; set;}
        public string WorkflowID { get; set; }
        
        public List<String> WorkflowIDs { get; set;}
        public Int32? Type { get; set; }
        
    }
}
