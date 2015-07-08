using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Model
{
    public class Activity
    {
        public string ID { get; set; }

        public string WorkflowID { get; set; }

        public string ActivityDefinitionID { get; set; }

        public string Page { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ProcessTime { get; set; }

    }
}
