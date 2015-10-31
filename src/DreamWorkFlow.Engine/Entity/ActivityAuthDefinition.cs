using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public partial class ActivityAuthDefinition : SimpleEntity
    {
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
        public string ActivityDefinitionID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowDefinitionID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? IsDeleted { get; set; }
        
    }
}