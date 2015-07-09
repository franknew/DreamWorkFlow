using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Model
{
    public class Activity : BaseEntity
    {

        public string WorkflowID { get; set; }

        public string ActivityDefinitionID { get; set; }

        public string Page { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public DateTime? ProcessTime { get; set; }

    }
}
