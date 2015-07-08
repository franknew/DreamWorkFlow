using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Model
{
    public class Workflow : BaseEntity
    {

        public string WorkflowDefinitionID { get; set; }

        public int? Status { get; set; }
    }
}
