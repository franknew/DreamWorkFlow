using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using SOAFramework.Library.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace DreamWorkflow.Engine
{
    public class WorkflowModel
    {
        private static ICache cache = CacheFactory.Create();
        public ActivityModel Root { get; set; }

        public List<Approval> Approval { get; set; }

        private Workflow value = new Workflow();

        public Workflow Value
        {
            get { return this.value; }
            private set { this.value = value; }
        }

        public static WorkflowModel Load(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            #region query data
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
            List<Approval> approval = new List<Approval>();
            WorkflowModel model = null;
            Workflow workflow = null;
            var item = cache.GetItem(id);
            if (item != null)
            {
                model = item.Value as WorkflowModel;
                return model;
            }
            try
            {
                workflow = wfdao.Query(new WorkflowQueryForm { ID = id }).FirstOrDefault();
                activitylist = activitydao.Query(new ActivityQueryForm { WorkflowID = id });
                linkList = linkDao.Query(new LinkQueryForm { WorkflowID = id });
                approval = approvalDao.Query(new ApprovalQueryForm { WorkflowID = id });
                authList = authdao.Query(new ActivityAuthQueryForm { WorkflowID = id });
                mapper.CommitTransaction();
            }
            catch
            {
                mapper.CommitTransaction();
                throw;
            }
            #endregion

            #region set data
            if (workflow != null)
            {
                List<ActivityModel> activityInstanceList = new List<ActivityModel>();
                List<LinkModel> linkModelList = new List<LinkModel>();
                model = new WorkflowModel
                {
                    value = workflow,
                    Approval = approval,
                };
                bool foundRoot = false;
                foreach (var link in linkList)
                {
                    linkModelList.Add(new LinkModel
                    {
                        Value = link,
                    });
                }
                foreach (var activity in activitylist)
                {
                    //pre link
                    var preLink = linkModelList.FindAll(t => t.Value.ToAcivityID == activity.ID);
                    //next link
                    var nextLink = linkModelList.FindAll(t => t.Value.FromActivityID == activity.ID);

                    var activityInstance = new ActivityModel
                    {
                        Value = activity,
                        Approvals = model.Approval.FindAll(t => t.ActivityID == activity.ID),
                        PreLinks = preLink,
                        NextLinks = nextLink,
                    };

                    //处理权限
                    var activityauth = authList.FindAll(t => t.ActivityID == activity.ID);
                    activityInstance.Auth.AddRange(activityauth);
                    //给审批排序
                    activityInstance.Approvals.Sort((l, r) =>
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
                    if (!foundRoot && preLink.Count == 0)
                    {
                        model.Root = activityInstance;
                        foundRoot = true;
                    }
                    activityInstanceList.Add(activityInstance);
                }

                item = new CacheItem(id, model);
                cache.AddItem(item, 30 * 60);
            }
            #endregion

            return model;
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
                    var activityinstance = activity as ActivityModel;
                    //保存连接
                    if (activityinstance.PreLinks != null)
                    {
                        foreach (var link in activityinstance.PreLinks)
                        {
                            if (!linkList.Contains(link.Value))
                            {
                                linkList.Add(link.Value);
                            }
                        }
                    }
                    if (activityinstance.NextLinks != null)
                    {
                        foreach (var link in activityinstance.NextLinks)
                        {
                            if (!linkList.Contains(link.Value))
                            {
                                linkList.Add(link.Value);
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

        public ActivityModel CurrentActivity
        {
            get
            {
                return null;
            }
        }
    }
}
