using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public class Task : SimpleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string AcitivityID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        
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
        public string UserID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        
    }
}