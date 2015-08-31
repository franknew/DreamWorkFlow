
using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine
{
    public class ActivityModel : Node<Activity>
    {
        private List<LinkModel> preLinks = new List<LinkModel>();

        public List<LinkModel> PreLinks
        {
            get { return preLinks; }
            set
            {
                preLinks = value;
                foreach (var link in value)
                {
                    link.ToActivity = this;
                }
            }
        }

        private List<LinkModel> nextLinks = new List<LinkModel>();

        public List<LinkModel> NextLinks
        {
            get { return nextLinks; }
            set
            {
                nextLinks = value;
                foreach (var link in value)
                {
                    link.FromActivity = this;
                }
            }
        }

        private List<Approval> approvals = new List<Approval>();

        public List<Approval> Approvals
        {
            get { return approvals; }
            set { approvals = value; }
        }

        private List<ActivityAuth> auth = new List<ActivityAuth>();

        public List<ActivityAuth> Auth
        {
            get { return auth; }
            set { auth = value; }
        }

        private List<Task> tasks = new List<Task>();

        public List<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        /// <summary>
        /// 处理指定活动点
        /// </summary>
        /// <param name="approval"></param>
        /// <param name="processor"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        public bool Process(Approval approval, string processor, IWorkflowAuthority auth)
        {
            ISqlMapper mapper = MapperHelper.GetMapper();
            ActivityDao activitydao = new ActivityDao(mapper);
            TaskDao taskdao = new TaskDao(mapper);
            this.Value.Status = (int)ActivityProcessStatus.Processed;
            this.Value.ProcessTime = DateTime.Now;
            this.Value.LastUpdator = processor;
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
                Entity = new Activity { Status = this.Value.Status, ProcessTime = this.Value.ProcessTime, LastUpdator = this.Value.LastUpdator },
                ActivityQueryForm = new ActivityQueryForm { ID = this.Value.ID }
            });

            //设置当前节点任务为已处理
            foreach (var task in Tasks)
            {
                taskdao.Update(new TaskUpdateForm
                {
                    Entity = new Task { ProcessTime = DateTime.Now, Status = (int)TaskProcessStatus.Processed },
                    TaskQueryForm = new TaskQueryForm { ID = task.ID },
                });
            }
            //设置下个活动点的状态
            if (this.Children.Count > 0)
            {
                string nextactivityid = this.Children[0].Value.ID;
                var nextActivityModel = this.Children[0] as ActivityModel;
                activitydao.Update(new ActivityUpdateForm
                {
                    Entity = new Activity { Status = (int)ActivityProcessStatus.Processing, LastUpdator = processor },
                    ActivityQueryForm = new ActivityQueryForm { ID = nextactivityid },
                });
                //新增下个活动点的任务
                var userlist = auth.GetUserIDList(nextActivityModel.Auth);
                var tasklist = GetTask(processor, userlist);
                foreach (var task in tasklist)
                {
                    taskdao.Add(task);
                }
            }

            return true;
        }

        public List<Task> GetTask(string processor, List<string> useridlist)
        {
            List<Task> list = new List<Task>();
            foreach (var id in useridlist)
            {
                Task task = new Task
                {
                    Name = this.Value.Name,
                    Title = this.Value.Title,
                    Status = (int)TaskProcessStatus.Started,
                    UserID = id,
                    AcitivityID = this.Value.ID,
                    Creator = processor,
                    WorkflowID = this.Value.WorkflowID,
                };
                list.Add(task);
            }
            return list;
        }
    }

}
