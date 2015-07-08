﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Model
{
    public class Workflow
    {
        public string ID { get; set; }

        public string WorkflowDefinitionID { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? Status { get; set; }
    }
}
