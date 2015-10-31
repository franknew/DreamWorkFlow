using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public partial class Parameter : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string ContextID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
        
    }
}