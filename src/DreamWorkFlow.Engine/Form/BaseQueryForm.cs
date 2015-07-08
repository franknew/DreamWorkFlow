﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Form
{
    public class BaseQueryForm
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }

        public DateTime? CreateTime_Start { get; set; }

        public DateTime? CreateTime_End { get; set; }
    }
}
