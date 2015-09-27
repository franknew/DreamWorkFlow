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
        public void Process(ActivityModel activity, Approval approval, string taskid, string processor, List<string> useridList)
        {
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
                ad.Add(approval);
            }
            //设置当前活动点状态
            activitydao.Update(new ActivityUpdateForm
            {
                Entity = new Activity { Status = activity.Value.Status, ProcessTime = activity.Value.ProcessTime, LastUpdator = activity.Value.LastUpdator },
                ActivityQueryForm = new ActivityQueryForm { ID = activity.Value.ID }
            });
            //处理任务
            var task = activity.Tasks.Find(t => t.ID == taskid);
            if (task != null)
            {
                taskdao.Update(new TaskUpdateForm
                {
                    Entity = new Task { ProcessTime = DateTime.Now, Status = (int)TaskProcessStatus.Processed },
                    TaskQueryForm = new TaskQueryForm { ID = task.ID },
                });
            }
            //设置下个活动点的状态
            if (activity.Children.Count > 0)
            {
                string nextactivityid = activity.Children[0].Value.ID;
                var nextActivityModel = activity.Children[0] as ActivityModel;
                activitydao.Update(new ActivityUpdateForm
                {
                    Entity = new Activity { Status = (int)ActivityProcessStatus.Processing, LastUpdator = processor },
                    ActivityQueryForm = new ActivityQueryForm { ID = nextactivityid },
                });
                //新增下个活动点的任务
                var tasklist = nextActivityModel.GetTask(processor, useridList);
                foreach (var t in tasklist)
                {
                    taskdao.Add(t);
                }
            }
        }
    }
}
