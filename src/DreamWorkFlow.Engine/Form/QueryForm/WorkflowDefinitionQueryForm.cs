using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class WorkflowDefinitionQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public UInt64? Enabled { get; set; }
        
        public string Remark { get; set; }
        
        public List<String> Creators { get; set;}
        public UInt64? IsDeleted { get; set; }
        
    }
}
