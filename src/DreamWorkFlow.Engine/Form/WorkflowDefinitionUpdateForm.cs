using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Form
{
    public class WorkflowDefinitionUpdateForm
    {
        public WorkflowDefinition WorkflowDefinition { get; set; }

        public WorkflowDefinitionQueryForm WorkflowDefinitionQueryForm { get; set; }
    }
}