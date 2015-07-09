using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Form
{
    public class ActivityAuthDefinitionQueryForm : BaseQueryForm
    {
        public string Type { get; set; }
        
        public string Value { get; set; }
        
        public string ActivityDefinitionID { get; set; }
        
    }
}
