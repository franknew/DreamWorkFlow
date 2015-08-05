using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class ActivityQueryForm : SimpleQueryForm
    {
        public string ActivityDefinitionID { get; set; }
        
        public string Page { get; set; }
        
        public Int32? Type { get; set; }
        
        public string WorkflowID { get; set; }
        
        public Int32? Status { get; set; }
        
        public DateTime? ReadTime_Start { get; set; }
        
        public DateTime? ReadTime_End { get; set; }
        
        public DateTime? ProcessTime_Start { get; set; }
        
        public DateTime? ProcessTime_End { get; set; }
        
        public string Title { get; set; }
        
    }
}
