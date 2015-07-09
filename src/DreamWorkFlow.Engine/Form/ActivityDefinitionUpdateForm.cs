using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Form
{
    public class ActivityDefinitionUpdateForm
    {
        public ActivityDefinition ActivityDefinition { get; set; }

        public ActivityDefinitionQueryForm ActivityDefinitionQueryForm { get; set; }
    }
}