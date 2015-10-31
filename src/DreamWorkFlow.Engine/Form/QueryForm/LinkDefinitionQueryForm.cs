using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class LinkDefinitionQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public string FromActivityDefinitionID { get; set; }
        
        public string ToActivityDefinitionID { get; set; }
        
        public List<String> Creators { get; set;}
        public string Remark { get; set; }
        
        public string WorkflowDefinitionID { get; set; }
        
        public UInt64? IsDeleted { get; set; }
        
    }
}
