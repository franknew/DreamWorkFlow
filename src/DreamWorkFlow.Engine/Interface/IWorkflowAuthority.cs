using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine
{
    public interface IWorkflowAuthority
    {
        List<string> GetUserIDList(List<ActivityAuth> auth);
    }
}
