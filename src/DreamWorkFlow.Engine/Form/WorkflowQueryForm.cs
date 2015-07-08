using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Form
{
    public class WorkflowQueryForm : BaseQueryForm
    {

        public string WorkflowDefinitionID { get; set; }


        public int? Status { get; set; }
    }
}
