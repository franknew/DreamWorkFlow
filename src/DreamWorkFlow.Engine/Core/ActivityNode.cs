
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine
{
    public class ActivityNode : Node<Activity>
    {
        public List<Link> Link { get; set; }

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
