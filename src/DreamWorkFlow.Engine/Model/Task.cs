using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkFlow.Engine.Model
{
    public class Task
    {
        public string ID { get; set; }

        public string ActivityID { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remark { get; set; }

        public DateTime ReadTime { get; set; }

        public DateTime ProcessTime { get; set; }
    }
}
