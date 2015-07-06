using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkFlow.Engine.Model
{
    public class ActivityDefinition
    {
        public string ID { get; set; }

        public string WorkflowDefinitionID { get; set; }

        public string Page { get; set; }

        public bool Enabled { get; set; }

        public string Type { get; set; }

        public string Remark { get; set; }

        public string Creator { get; set; }

        public string CreateTime { get; set; }
    }
}
