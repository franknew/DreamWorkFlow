using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Model
{
    public class Approval : BaseEntity
    {
        public string ActivityID { get; set; }
        
        public Boolean? Passed { get; set; }
        
        public string Remark { get; set; }
        
    }
}