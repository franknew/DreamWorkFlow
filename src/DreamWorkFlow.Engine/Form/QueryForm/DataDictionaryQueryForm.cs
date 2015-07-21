using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Form
{
    public class DataDictionaryQueryForm : BaseQueryForm
    {
        public Int32? Value { get; set; }
        
        public string Remark { get; set; }
        
        public DateTime? LastUpdateTime_Start { get; set; }
        
        public DateTime? LastUpdateTime_End { get; set; }
        
        public string LastUpdator { get; set; }
        
        public string DataDictionaryGroupID { get; set; }
        
    }
}
