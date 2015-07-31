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
    public class WorkflowInstance
    {
        public ActivityInstance Root { get; set; }

        public List<Approval> Approval { get; set; }

        private Workflow value = new Workflow();

        public Workflow Value
        {
            get { return this.value; }
            private set { this.value = value; }
        }

        public static WorkflowInstance Load(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return null;
            }
            #region query data
            WorkflowInstance workflowinstance = new WorkflowInstance();
            ISqlMapper mapper = Mapper.Instance();
            mapper.BeginTransaction();
            WorkflowDao wfdao = new WorkflowDao(mapper);
            ActivityDao activitydao = new ActivityDao(mapper);
            LinkDao linkDao = new LinkDao(mapper);
            ActivityAuthDao authdao = new ActivityAuthDao(mapper);
            ApprovalDao approvalDao = new ApprovalDao(mapper);
            List<Activity> activitylist = new List<Activity>();
            List<Link> linkList = new List<Link>();
            List<ActivityAuth> authList = new List<ActivityAuth>();
            try
            {
                workflowinstance.value = wfdao.Query(new WorkflowQueryForm { ID = id }).FirstOrDefault();
                activitylist = activitydao.Query(new ActivityQueryForm { WorkflowID = id });
                linkList = linkDao.Query(new LinkQueryForm { WorkflowID = id });
                workflowinstance.Approval = approvalDao.Query(new ApprovalQueryForm { WorkflowID = id });
                authList = authdao.Query(new ActivityAuthQueryForm { WorkflowID = id });
                mapper.CommitTransaction();
            }
            catch
            {
                mapper.RollBackTransaction();
                throw;
            }
            #endregion

            #region set data
            List<ActivityInstance> activityInstanceList = new List<ActivityInstance>();
            foreach (var activity in activitylist)
            {
                //pre link
                var preLink = linkList.FindAll(t => t.ToAcivityID == activity.ID);
                //next link
                var nextLink = linkList.FindAll(t => t.FromActivityID == activity.ID);
                var activityInstance = new ActivityInstance
                {
                    Value = activity,
                    Approvals = workflowinstance.Approval.FindAll(t => t.ActivityID == activity.ID),
                    Children = new List<Node<Activity>>(),
                    Parents = new List<Node<Activity>>(),
                };
                activityInstance.PreLinks = new List<LinkInstance>();
                activityInstance.NextLinks = new List<LinkInstance>();
                //处理前连接
                foreach (var link in preLink)
                {
                    var linkInstance = new LinkInstance
                    {
                        Link = link,
                        FromActivity = activitylist.Find(t => t.ID == link.FromActivityID),
                        ToActivity = activity,
                    };
                    activityInstance.PreLinks.Add(linkInstance);
                }
                //处理后连接
                foreach (var link in nextLink)
                {
                    var linkInstance = new LinkInstance
                    {
                        Link = link,
                        ToActivity = activitylist.Find(t => t.ID == link.ToAcivityID),
                        FromActivity = activity,
                    };
                    activityInstance.NextLinks.Add(linkInstance);
                }

                if (preLink == null || preLink.Count == 0)
                {
                    workflowinstance.Root = activityInstance;
                }
                //处理权限
                activityInstance.Auth = new List<ActivityAuth>();
                var activityauth = authList.FindAll(t => t.ActivityID == activity.ID);
                activityInstance.Auth.AddRange(activityauth);
                //给审批排序
                activityInstance.Approvals.Sort((l, r)=>
                {
                    if (l.CreateTime > l.CreateTime)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                });

                activityInstanceList.Add(activityInstance);
            }

            //设置parent和children
            foreach (var instance in activityInstanceList)
            {
                if (instance.PreLinks != null)
                {
                    foreach (var link in instance.PreLinks)
                    {
                        var parent = activityInstanceList.Find(t => t.Value.ID == link.FromActivity.ID);
                        instance.Parents.Add(instance);
                    }
                }

                if (instance.NextLinks != null)
                {
                    foreach (var link in instance.NextLinks)
                    {
                        var child = activityInstanceList.Find(t => t.Value.ID == link.ToActivity.ID);
                        instance.Children.Add(instance);
                    }
                }
            }
            #endregion

            return workflowinstance;
        }

        public bool Create()
        {
            ISqlMapper mapper = Mapper.Instance();
            WorkflowDao workflowdao = new WorkflowDao(mapper);
            ActivityDao activitydao = new ActivityDao(mapper);
            LinkDao linkdao = new LinkDao(mapper);
            ActivityAuthDao authdao = new ActivityAuthDao(mapper);
            try
            {
                mapper.BeginTransaction();
                workflowdao.Add(this.Value);
                var activities = this.Root.GetList();
                List<Link> linkList = new List<Link>();
                //保存节点
                foreach (var activity in activities)
                {
                    activitydao.Add(activity.Value);
                    var activityinstance = activity as ActivityInstance;
                    //保存连接
                    if (activityinstance.PreLinks != null)
                    {
                        foreach (var link in activityinstance.PreLinks)
                        {
                            if (!linkList.Contains(link.Link))
                            {
                                linkList.Add(link.Link);
                            }
                        }
                    }
                    if (activityinstance.NextLinks != null)
                    {
                        foreach (var link in activityinstance.NextLinks)
                        {
                            if (!linkList.Contains(link.Link))
                            {
                                linkList.Add(link.Link);
                            }
                        }
                    }
                    //保存权限
                    foreach (var auth in activityinstance.Auth)
                    {
                        authdao.Add(auth);
                    }
                }
                foreach (var link in linkList)
                {
                    linkdao.Add(link);
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

        public ActivityInstance CurrentActivity
        {
            get
            {
                return null;
            }
        }
    }
}
