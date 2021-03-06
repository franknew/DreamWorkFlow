using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public class Menu : SimpleEntity
    {
        public string Page { get; set; }
        
        public string ParentID { get; set; }
        
        public UInt64? Enabled { get; set; }
        
        public string Remark { get; set; }
        
    }
}