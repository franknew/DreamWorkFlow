using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Form
{
    public class WorkflowDefinitionQueryForm : BaseQueryForm
    {
        public bool? Enabled { get; set; }

        public string Remark { get; set; }
    }
}
