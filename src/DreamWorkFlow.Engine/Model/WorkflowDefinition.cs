using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Model
{
    public class WorkflowDefinition : SimpleEntity
    {
        public Boolean? Enabled { get; set; }
        
        public string Remark { get; set; }
        
    }
}