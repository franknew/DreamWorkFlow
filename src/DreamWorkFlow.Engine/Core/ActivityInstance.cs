
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine
{
    public class ActivityInstance : Node<Activity>
    {
        public List<LinkInstance> LinkFrom { get; set; }

        public List<LinkInstance> LinkTo { get; set; }

        public string Save(Mapper mapper)
        {
            return null;
        }

        public bool Delete(Mapper mapper)
        {
            return false;
        }
    }
}
