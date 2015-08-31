using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public class Workflow : SimpleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowDefinitionID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Status { get; set; }
        
        /// <summary>
        /// 外接项目ID
        /// </summary>
        public string ProcessID { get; set; }
        
    }
}