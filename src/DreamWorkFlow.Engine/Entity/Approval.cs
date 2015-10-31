using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public partial class Approval : SimpleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string ActivityID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Status { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? Type { get; set; }
        
    }
}