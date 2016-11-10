
using DreamWorkflow.Engine.Core;
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

        public WorkflowModel OwnerWorkflow { get; set; }

        /// <summary>
        /// 处理指定活动点
        /// </summary>
        /// <param name="approval"></param>
        /// <param name="processor"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        public bool Process(Approval approval, string processor, IWorkflowAuthority auth)
        {
            IProcessAction actionprocess = ApprovalProcessFacotry.Create((ApprovalStatus)approval.Status.Value);
            actionprocess.Process(this, approval, processor, auth);
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
                    ActivityID = this.Value.ID,
                    Creator = processor,
                    WorkflowID = this.Value.WorkflowID,
                };
                list.Add(task);
            }
            return list;
        }

        public void ReadTask(string taskid, string proccessor)
        {
            ISqlMapper mapper = MapperHelper.GetMapper();
            TaskDao taskdao = new TaskDao(mapper);
            var task = this.Tasks.Find(t => t.ID == taskid);
            if (this.Value.Status == (int)ActivityProcessStatus.Processed)
            {
                task.Status = (int)TaskProcessStatus.Processed;
            }
            else
            {
                task.Status = (int)TaskProcessStatus.Read;
            }
            task.ReadTime = DateTime.Now;
            task.LastUpdator = proccessor;
            taskdao.Update(new TaskUpdateForm
            {
                Entity = new Task { ReadTime = task.ReadTime, Status = task.Status, LastUpdator = task.LastUpdator },
                TaskQueryForm = new TaskQueryForm { ID = task.ID }
            });
        }

        public bool CanUserProcess(string processor)
        {
            return this.tasks.Exists(t => t.UserID.Equals(processor) && t.Status == (int)TaskProcessStatus.Started);
        }

        public Task GetUserProcessingTask(string processor)
        {
            return this.tasks.Find(t => t.UserID.Equals(processor) && t.Status == (int)TaskProcessStatus.Started);
        }
    }

}
