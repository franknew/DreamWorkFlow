using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamWorkflow.Engine.Model;

namespace DreamWorkflow.Engine
{
    public class LinkInstance
    {
        public Activity FromActivity { get; set; }

        public Activity ToActivity { get; set; }

        public Link Link { get; set; }
    }
}
