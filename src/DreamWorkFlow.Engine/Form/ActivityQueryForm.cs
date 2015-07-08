using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Form
{
    public class ActivityQueryForm : BaseQueryForm
    {
        public string ActivityDefinitionID { get; set; }


        public int? Status { get; set; }

        public string Page { get; set; }

        public int? Type { get; set; }

        public string WorkflowID { get; set; }

        public DateTime? ProcessTime_Start { get; set; }

        public DateTime? ProcessTime_End { get; set; }
    }
}
