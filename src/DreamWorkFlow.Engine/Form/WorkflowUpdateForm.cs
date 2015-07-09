using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Form
{
    public class WorkflowUpdateForm
    {
        public Workflow Workflow { get; set; }

        public WorkflowQueryForm WorkflowQueryForm { get; set; }
    }
}