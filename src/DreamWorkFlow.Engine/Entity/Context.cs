using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public partial class Context : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowID { get; set; }
        
    }
}