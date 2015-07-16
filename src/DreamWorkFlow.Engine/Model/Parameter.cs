using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public class Parameter : BaseEntity
    {
        public string ContextID { get; set; }
        
        public string Key { get; set; }
        
        public string Value { get; set; }
        
    }
}