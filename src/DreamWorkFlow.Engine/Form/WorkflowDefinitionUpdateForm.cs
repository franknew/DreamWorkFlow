using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class WorkflowDefinitionUpdateForm : SimpleUpdateForm<WorkflowDefinition>
    {
        public WorkflowDefinitionQueryForm WorkflowDefinitionQueryForm { get; set; }
    }
}