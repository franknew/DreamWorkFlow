using DreamWorkflow.Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using SOAFramework.Library;

namespace DreamWorkflow.Engine
{
    public class NextProcessAction : IProcessAction
    {
        public void Process(ActivityModel activity, Approval approval, string processor, IWorkflowAuthority auth)
        {
            //已经处理过就不能再处理
            if (activity.Value.Status == (int)ActivityProcessStatus.Processed) return;
            //MonitorCache.GetInstance().PushMessage(new CacheMessage { Message = "activityid:" + activity.Value.ID + " next activity status:" + activity.Value.Status.ToString() }, CacheEnum.FormMonitor);
            ISqlMapper mapper = MapperHelper.GetMapper();
            ActivityDao activitydao = new ActivityDao(mapper);
            TaskDao taskdao = new TaskDao(mapper);
            //设置当前活动点状态
            activity.Value.Status = (int)ActivityProcessStatus.Processed;
            activity.Value.ProcessTime = DateTime.Now;
            activity.Value.LastUpdator = processor;
            activitydao.Update(new ActivityUpdateForm
            {
                Entity = new Activity { Status = activity.Value.Status, ProcessTime = activity.Value.ProcessTime, LastUpdator = activity.Value.LastUpdator },
                ActivityQueryForm = new ActivityQueryForm { ID = activity.Value.ID }
            });
            var task = activity.GetUserProcessingTask(processor);
            if (task == null) throw new Exception("环节中没有你的任务，无法进行审批操作");
            task.Status = (int)TaskProcessStatus.Processed;
            task.ProcessTime = DateTime.Now;
            task.LastUpdator = processor;
            //处理任务
            taskdao.Update(new TaskUpdateForm
            {
                Entity = new Task { ProcessTime = task.ProcessTime, Status = task.Status, LastUpdator = task.LastUpdator },
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
                        nextActivityModel.Tasks.Add(t);
                        taskdao.Add(t);
                    }
                }
            }
        }
    }
}
