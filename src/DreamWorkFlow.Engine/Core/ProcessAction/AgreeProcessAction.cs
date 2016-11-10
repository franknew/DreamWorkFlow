using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamWorkflow.Engine.Model;
using DreamWorkflow.Engine.DAL;
using IBatisNet.DataMapper;
using DreamWorkflow.Engine.Form;

namespace DreamWorkflow.Engine.Core
{
    public class AgreeProcessAction : IProcessAction
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
            activity.Value.Status = (int)ActivityProcessStatus.Processed;
            activity.Value.ProcessTime = DateTime.Now;
            activity.Value.LastUpdator = processor;
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
            //设置当前活动点状态
            activitydao.Update(new ActivityUpdateForm
            {
                Entity = new Activity { Status = activity.Value.Status, ProcessTime = activity.Value.ProcessTime, LastUpdator = activity.Value.LastUpdator },
                ActivityQueryForm = new ActivityQueryForm { ID = activity.Value.ID }
            });
            //处理任务
            var task = activity.GetUserProcessingTask(processor);
            if (task == null) throw new Exception("环节中没有你的任务，无法进行审批操作");
            task.ProcessTime = DateTime.Now;
            task.Status = (int)TaskProcessStatus.Processed;
            task.LastUpdator = processor;
            taskdao.Update(new TaskUpdateForm
            {
                Entity = new Task { ProcessTime = DateTime.Now, Status = task.Status, LastUpdator = task.LastUpdator },
                TaskQueryForm = new TaskQueryForm { ID = task.ID },
            });
            //设置下个活动点的状态
            if (activity.Children.Count > 0)
            {
                foreach (var next in activity.Children)
                {
                    string nextactivityid = next.Value.ID;
                    var nextActivityModel = next as ActivityModel;
                    nextActivityModel.Value.Status = (int)ActivityProcessStatus.Processing;
                    nextActivityModel.Value.LastUpdator = processor;
                    activitydao.Update(new ActivityUpdateForm
                    {
                        Entity = new Activity { Status = nextActivityModel.Value.Status, LastUpdator = nextActivityModel.Value.LastUpdator },
                        ActivityQueryForm = new ActivityQueryForm { ID = nextactivityid },
                    });

                    List<string> useridList = auth.GetUserIDList(nextActivityModel.Auth);
                    //新增下个活动点的任务
                    var tasklist = nextActivityModel.GetTask(processor, useridList);
                    foreach (var t in tasklist)
                    {
                        taskdao.Add(t);
                        nextActivityModel.Tasks.Add(t);
                    }
                }
            }
        }
    }
}
