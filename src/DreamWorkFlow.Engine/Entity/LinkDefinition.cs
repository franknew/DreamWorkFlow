using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public partial class LinkDefinition : SimpleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string FromActivityDefinitionID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ToActivityDefinitionID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        
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