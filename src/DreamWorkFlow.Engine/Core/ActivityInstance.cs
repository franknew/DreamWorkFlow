
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
        public List<LinkInstance> PreLinks { get; set; }

        public List<LinkInstance> NextLinks { get; set; }

        public List<Approval> Approvals { get; set; }

        public List<ActivityAuth> Auth { get; set; }

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
