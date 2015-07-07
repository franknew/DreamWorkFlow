using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DreamWorkFlow.Engine.Model
{
    public class Link
    {
        public string ID { get; set; }

        public string FromAcitivityID { get; set; }

        public string ToActivityID { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        
    }
}
