using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public partial class Activity : SimpleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string ActivityDefinitionID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Page { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Type { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Status { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReadTime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ProcessTime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        
    }
}