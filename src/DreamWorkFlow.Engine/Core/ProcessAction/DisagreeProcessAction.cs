using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;

namespace DreamWorkflow.Engine.Core
{
    public class DisagreeProcessAction : IProcessAction
    {
        public void Process(ActivityModel activity, Approval approval, string processor, IWorkflowAuthority auth)
        {
            if (approval == null) throw new Exception("审批意见不能为null");
            if (string.IsNullOrEmpty(approval.Remark)) throw new Exception("审批意见不能为空");
            //已经处理过就不能再处理
            if (activity.Value.Status == (int)ActivityProcessStatus.Processed) return;
            ISqlMapper mapper = MapperHelper.GetMapper();
            ActivityDao activitydao = new ActivityDao(mapper);
            TaskDao taskdao = new TaskDao(mapper);
            activity.Value.Status = (int)ActivityProcessStatus.Started;
            activity.Value.ProcessTime = DateTime.Now;
            activity.Value.LastUpdator = processor;

            //设置当前活动点状态
            activitydao.Update(new ActivityUpdateForm
            {
                Entity = new Activity { Status = activity.Value.Status, ProcessTime = activity.Value.ProcessTime, LastUpdator = activity.Value.LastUpdator },
                ActivityQueryForm = new ActivityQueryForm { ID = activity.Value.ID }
            });
            //新增审批意见
            if (approval != null)
            {
                ApprovalDao ad = new ApprovalDao(mapper);
                approval.Creator = processor;
                approval.ActivityID = activity.Value.ID;
                approval.WorkflowID = activity.Value.WorkflowID;
                ad.Add(approval);
                activity.OwnerWorkflow.Approval.Add(approval);
            }
            //处理当前流程所有任务，设置为已处理
            var task = activity.GetUserProcessingTask(processor);
            if (task == null) throw new Exception("环节中没有你的任务，无法进行审批操作");
            task.ProcessTime = DateTime.Now;
            task.Status = (int)TaskProcessStatus.Processed;
            task.LastUpdator = processor;
            taskdao.Update(new TaskUpdateForm
            {
                Entity = new Task { ProcessTime = task.ProcessTime, Status = task.Status, LastUpdator = task.LastUpdator },
                TaskQueryForm = new TaskQueryForm { ActivityID = task.ActivityID },
            });
            //把所有活动点的状态清空
            activity.OwnerWorkflow.Root.GetList().ForEach(t => t.Value.Status = activity.Value.Status);
            activitydao.Update(new ActivityUpdateForm
            {
                Entity = new Activity { Status = activity.Value.Status },
                ActivityQueryForm = new ActivityQueryForm { WorkflowID = activity.Value.WorkflowID }
            });
            activity.OwnerWorkflow.Root.Value.Status = (int)ActivityProcessStatus.Processing;
            var root = activity.OwnerWorkflow.Root;
            activitydao.Update(new ActivityUpdateForm
            {
                Entity = new Activity { Status = root.Value.Status },
                ActivityQueryForm = new ActivityQueryForm { ID = root.Value.ID },
            });
            //生成退回任务
            Task roottask = new Task
            {
                ActivityID = root.Value.ID,
                Name = root.Value.Name,
                Title = root.Value.Title + "(退回)",
                UserID = activity.OwnerWorkflow.Value.Creator,
                WorkflowID = activity.OwnerWorkflow.Value.ID,
                Status = (int)TaskProcessStatus.Started,
                Creator = processor,
            };
            root.Tasks.Add(roottask);
            taskdao.Add(roottask);
        }
    }
}
