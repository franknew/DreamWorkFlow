using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Model
{
    public class WorkflowDefinition : BaseEntity
    {
        public string Remark { get; set; }

        public bool Enabled { get; set; }
    }
}
