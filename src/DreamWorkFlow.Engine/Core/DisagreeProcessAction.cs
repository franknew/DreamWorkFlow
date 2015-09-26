﻿using System;
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
            //处理当前任务
            var task = activity.Tasks.Find(t => t.ID == taskid);
            if (task != null)
            {
                task.ProcessTime = DateTime.Now;
                task.Status = (int)TaskProcessStatus.Processed;
                taskdao.Update(new TaskUpdateForm
                {
                    Entity = new Task { ProcessTime = task.ProcessTime, Status = task.Status },
                    TaskQueryForm = new TaskQueryForm { ID = task.ID },
                });
            }
            //设置当前活动点状态
            activitydao.Update(new ActivityUpdateForm
            {
                Entity = new Activity { Status = activity.Value.Status, ProcessTime = activity.Value.ProcessTime, LastUpdator = activity.Value.LastUpdator },
                ActivityQueryForm = new ActivityQueryForm { ID = activity.Value.ID }
            });
            activity.OwnerWorkflow.Root.Value.Status = (int)ActivityProcessStatus.Started;
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
            };
            taskdao.Add(roottask);
        }
    }
}