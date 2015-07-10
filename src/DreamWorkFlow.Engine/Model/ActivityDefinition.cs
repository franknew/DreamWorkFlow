using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Model
{
    public class ActivityDefinition : SimpleEntity
    {
        public string WorkflowDefinitionID { get; set; }
        
        public string Page { get; set; }
        
        public Boolean? Enabled { get; set; }
        
        public Int32? Type { get; set; }
        
        public string Remark { get; set; }
        
    }
}