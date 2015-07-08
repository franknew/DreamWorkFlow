using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Model
{
    public class LinkDefinition : BaseEntity
    {

        public string FromAcitivityDefinitionID { get; set; }

        public string ToActivityDefinitionID { get; set; }

        public string Remark { get; set; }
    }
}
