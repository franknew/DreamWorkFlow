using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Core
{
    public interface IProcessAction
    {
        void Process(ActivityModel activity, Approval approval, string taskid, string processor, List<string> useridList);
    }

    public class ApprovalProcessFacotry
    {
        public static IProcessAction Create(ApprovalStatus status)
        {
            IProcessAction action = null;
            switch (status)
            {
                case ApprovalStatus.Agree:
                    action = new AgreeProcessAction();
                    break;
                case ApprovalStatus.Disagree:
                    break;
            }
            return action;
        }
    }
}
