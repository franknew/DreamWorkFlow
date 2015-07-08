using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Model
{
    public class Task : BaseEntity
    {

        public string ActivityID { get; set; }

        public string Remark { get; set; }

        public DateTime ReadTime { get; set; }

        public DateTime ProcessTime { get; set; }
    }
}
