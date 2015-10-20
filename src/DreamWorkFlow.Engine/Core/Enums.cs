using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine
{
    public enum ActivityProcessStatus
    {
        Started = 1,
        Processing = 2,
        Processed = 3,
    }

    public enum WorkflowProcessStatus
    {
        Started = 1,
        Processing = 2,
        Processed = 3,
        Stoped = 4,
    }

    public enum TaskProcessStatus
    {
        Started = 1,
        Read = 2,
        Processed = 3,
    }

    public enum ApprovalStatus
    {
        Agree = 1,
        Disagree = 2,
        None = 3,
    }
}
