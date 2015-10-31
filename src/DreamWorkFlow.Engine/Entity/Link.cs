using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public partial class Link : SimpleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string LinkDefinitionID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string FromActivityID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ToAcivityID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Passed { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PassedTime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowID { get; set; }
        
    }
}