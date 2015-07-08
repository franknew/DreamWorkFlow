using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Model
{
    public class LinkDefinition
    {
        public string ID { get; set; }

        public string FromAcitivityDefinitionID { get; set; }

        public string ToActivityDefinitionID { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remark { get; set; }
    }
}
