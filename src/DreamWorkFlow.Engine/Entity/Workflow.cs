using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public class Workflow : SimpleEntity
    {
        public string WorkflowDefinitionID { get; set; }
        
        public Int32? Status { get; set; }
        
    }
}