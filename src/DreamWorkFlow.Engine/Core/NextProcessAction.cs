using DreamWorkflow.Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;

namespace DreamWorkflow.Engine
{
    public class NextProcessAction : IProcessAction
    {
        public void Process(ActivityModel activity, Approval approval, string taskid, string processor, IWorkflowAuthority auth)
        {
            ISqlMapper mapper = MapperHelper.GetMapper();
            ActivityDao activitydao = new ActivityDao(mapper);
            TaskDao taskdao = new TaskDao(mapper);
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
                foreach (var next in activity.Children)
                {
                    string nextactivityid = next.Value.ID;
                    var nextActivityModel = next as ActivityModel;
                    activitydao.Update(new ActivityUpdateForm
                    {
                        Entity = new Activity { Status = (int)ActivityProcessStatus.Processing, LastUpdator = processor },
                        ActivityQueryForm = new ActivityQueryForm { ID = nextactivityid },
                    });

                    List<string> useridList = auth.GetUserIDList(nextActivityModel.Auth);
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
}
