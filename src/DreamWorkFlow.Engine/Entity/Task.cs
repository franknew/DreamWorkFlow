using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public partial class Task : SimpleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string ActivityID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        
        /// <summary>
        /// 状态 1.启动 2.已读 3.已处理
        /// </summary>
        public int? Status { get; set; }
        
        /// <summary>
        /// 已读时间
        /// </summary>
        public DateTime? ReadTime { get; set; }
        
        /// <summary>
        /// 处理时间
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
        
        /// <summary>
        /// 
        /// </summary>
        public string WorkflowID { get; set; }
        
    }
}