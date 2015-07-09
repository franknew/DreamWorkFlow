using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Model
{
    public class ActivityAuth : BaseEntity
    {
        public string AcitivityAuthDefinitionID { get; set; }
        
        public string Type { get; set; }
        
        public string Value { get; set; }
        
        public string AcitivityID { get; set; }
        
    }
}