using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class DataDictionaryQueryForm : SimpleQueryForm
    {
        public List<String> IDs { get; set;}
        public Int32? Value { get; set; }
        
        public List<String> Creators { get; set;}
        public string Remark { get; set; }
        
        public string DataDictionaryGroupID { get; set; }
        
        public UInt64? IsDeleted { get; set; }
        
    }
}
