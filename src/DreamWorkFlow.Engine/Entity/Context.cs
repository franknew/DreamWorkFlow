using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public class Context : BaseEntity
    {
        public string WorkflowID { get; set; }
        
    }
}