using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class ContextQueryForm : BaseQueryForm
    {
        public List<String> IDs { get; set;}
        public string WorkflowID { get; set; }
        
        public List<String> WorkflowIDs { get; set;}
    }
}
