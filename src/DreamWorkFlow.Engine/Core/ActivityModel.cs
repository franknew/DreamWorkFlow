
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
        

        public bool Process(Approval approval, string processor, IWorkflowAuthority auth)
        {
            ISqlMapper mapper = Mapper.Instance();
            ActivityDao activitydao = new ActivityDao(mapper);
            TaskDao taskdao = new TaskDao(mapper);
            mapper.BeginTransaction();
            try
            {
                this.Value.Status = (int)ActivityProcessStatus.Processed;
                this.Value.ProcessTime = DateTime.Now;
                this.Value.LastUpdator = processor;
                if (approval != null)
                {
                    ApprovalDao ad = new ApprovalDao(mapper);
                    approval.Creator = processor;
                    ad.Add(approval);
                }
                activitydao.Update(new ActivityUpdateForm
                {
                    Entity = new Activity { Status = this.Value.Status, ProcessTime = this.Value.ProcessTime, LastUpdator = this.Value.LastUpdator },
                    ActivityQueryForm = new ActivityQueryForm { ID = this.Value.ID }
                });
                if (this.Children.Count > 0)
                {
                    string nextactivityid = this.Children[0].Value.ID;
                    activitydao.Update(new ActivityUpdateForm
                    {
                        Entity = new Activity { Status = (int)ActivityProcessStatus.Processing, LastUpdator = processor },
                        ActivityQueryForm = new ActivityQueryForm { ID = nextactivityid },
                    });
                }
                var userlist = auth.GetUserIDList(this.auth);
                var tasklist = GetTask(processor, userlist);
                foreach (var task in tasklist)
                {
                    taskdao.Add(task);
                }
                mapper.CommitTransaction();
            }
            catch
            {
                mapper.RollBackTransaction();
                throw;
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
                };
                list.Add(task);
            }
            return list;
        }
    }

}
