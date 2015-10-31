using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class TaskQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public string ActivityID { get; set; }
        
        public List<String> Creators { get; set;}
        public string Remark { get; set; }
        
        public Int32? Status { get; set; }
        
        public DateTime? ReadTime_Start { get; set; }
        
        public DateTime? ReadTime_End { get; set; }
        
        public DateTime? ProcessTime_Start { get; set; }
        
        public DateTime? ProcessTime_End { get; set; }
        
        public string UserID { get; set; }
        
        public string Title { get; set; }
        
        public string WorkflowID { get; set; }
        
        public List<String> WorkflowIDs { get; set;}
    }
}
