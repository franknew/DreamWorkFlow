using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public partial class ActivityAuth : SimpleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string ActivityAuthDefinitionID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ActivityID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowID { get; set; }
        
    }
}