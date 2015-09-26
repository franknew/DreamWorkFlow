
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
        public bool Process(Approval approval, string taskid, string processor, IWorkflowAuthority auth)
        {
            IProcessAction actionprocess = ApprovalProcessFacotry.Create((ApprovalStatus)approval.Status.Value);
            List<ActivityAuth> list = new List<ActivityAuth>();
            if (this.Children != null && this.Children.Count > 0)
            {
                list = (this.Children[0] as ActivityModel).Auth;
            }
            actionprocess.Process(this, approval, taskid, processor, auth.GetUserIDList(list));

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
    }

}
