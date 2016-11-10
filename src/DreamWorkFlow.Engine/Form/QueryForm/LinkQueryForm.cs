using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class LinkQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public string LinkDefinitionID { get; set; }
        
        public List<String> LinkDefinitionIDs { get; set;}
        public string FromActivityID { get; set; }
        
        public List<String> FromActivityIDs { get; set;}
        public string ToAcivityID { get; set; }
        
        public List<String> ToAcivityIDs { get; set;}
        public List<String> Creators { get; set;}
        public UInt64? Passed { get; set; }
        
        public DateTime? PassedTime_Start { get; set; }
        
        public DateTime? PassedTime_End { get; set; }
        
        public string WorkflowID { get; set; }
        
        public List<String> WorkflowIDs { get; set;}
    }
}
