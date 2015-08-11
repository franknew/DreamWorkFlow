using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library;
using SOAFramework.Library.Cache;
using System.Runtime.Caching;
using SOAFramework.Library;

namespace DreamWorkflow.Engine
{
    public class WorkflowDefinitionModel
    {
        private static ICache cache = CacheFactory.Create();

        public ActivityDefinitionModel Root { get; set; }

        private WorkflowDefinition value = new WorkflowDefinition();

        public WorkflowDefinition Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public List<ActivityDefinitionModel> ActivityDefinitionList
        {
            get
            {
                List<ActivityDefinitionModel> list = new List<ActivityDefinitionModel>();
                if (Root != null)
                {
                    var activities = Root.GetList();
                    foreach (var activity in activities)
                    {
                        list.Add(activity as ActivityDefinitionModel);
                    }
                }
                return list;
            }
        }

        public List<LinkDefinitionModel> LinkDefinitionList
        {
            get
            {
                List<LinkDefinitionModel> list = new List<LinkDefinitionModel>();
                var activities = ActivityDefinitionList;
                foreach (var activity in activities)
                {
                    foreach (var link in activity.PreLinks)
                    {
                        if (!list.Contains(link))
                        {
                            list.Add(link);
                        }
                    }
                    foreach (var link in activity.NextLinks)
                    {
                        if (!list.Contains(link))
                        {
                            list.Add(link);
                        }
                    }
                }
                return list;
            }
        }

        public bool Save()
        {
            ISqlMapper mapper = Mapper.Instance();
            WorkflowDefinitionDao wddao = new WorkflowDefinitionDao(mapper);
            ActivityAuthDefinitionDao aaddao = new ActivityAuthDefinitionDao(mapper);
            ActivityDefinitionDao addao = new ActivityDefinitionDao(mapper);
            LinkDefinitionDao linkdao = new LinkDefinitionDao(mapper);
            ActivityAuthDefinitionDao aadd = new ActivityAuthDefinitionDao(mapper);
            try
            {
                mapper.BeginTransaction();
                var workflowdefinition = wddao.Query(new WorkflowDefinitionQueryForm { ID = this.value.ID }).FirstOrDefault();
                if (workflowdefinition == null)
                {
                    wddao.Add(this.value);
                }
                else
                {
                    wddao.Update(new WorkflowDefinitionUpdateForm { Entity = this.value, WorkflowDefinitionQueryForm = new WorkflowDefinitionQueryForm { ID = this.value.ID } });
                }
                var activityList = this.Root.GetList();
                foreach (var a in activityList)
                {
                    var acitivitydefinitioninstance = a as ActivityDefinitionModel;
                    acitivitydefinitioninstance.Save(mapper);
                }

                foreach (var link in LinkDefinitionList)
                {
                    link.Save(mapper);
                }
                var item = cache.GetItem(this.value.ID);
                if (item == null)
                {
                    item = new CacheItem(this.value.ID, this);
                    cache.AddItem(item, 30 * 60);
                }
                else
                {
                    item.Value = this;
                    cache.UpdateItem(item);
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

        public WorkflowModel StartNew(string creator, IWorkflowAuthority iauth)
        {
            var mapper = Mapper.Instance();
            WorkflowDao workflowdao = new WorkflowDao(mapper);
            ActivityDao activitydao = new ActivityDao(mapper);
            LinkDao linkdao = new LinkDao(mapper);
            ActivityAuthDao aadd = new ActivityAuthDao(mapper);
            TaskDao taskdao = new TaskDao(mapper);
            WorkflowModel model = null;
            try
            {
                mapper.BeginTransaction();
                Workflow wf = this.value.ConvertTo<Workflow>();
                wf.ID = null;
                wf.Creator = creator;
                wf.Status = (int)WorkflowProcessStatus.Started;
                wf.WorkflowDefinitionID = this.value.ID;
                workflowdao.Add(wf);

                var activites = this.ActivityDefinitionList;
                var links = this.LinkDefinitionList;
                List<Activity> activityEntities = new List<Activity>();
                foreach (var a in activites)
                {
                    Activity activity = a.Value.ConvertTo<Activity>();
                    activity.Creator = creator;
                    activity.ID = null;
                    activity.WorkflowID = wf.ID;
                    activity.ActivityDefinitionID = a.Value.ID;
                    activity.Title = a.Value.Title;
                    activity.Status = (int)ActivityProcessStatus.Started;

                    List<ActivityAuth> authList = new List<ActivityAuth>();
                    //权限处理
                    foreach (var ad in a.AuthDefinition)
                    {
                        ActivityAuth auth = ad.ConvertTo<ActivityAuth>();
                        auth.Creator = creator;
                        auth.WorkflowID = wf.ID;
                        auth.ID = null;
                        auth.ActivityAuthDefinitionID = ad.ID;
                        aadd.Add(auth);
                        authList.Add(auth);
                    }

                    //如果是开始节点，就设置为已处理
                    if (this.Root.Equals(a))
                    {
                        activity.Status = (int)ActivityProcessStatus.Processed;
                        activity.ProcessTime = DateTime.Now;
                    }
                    //如果是第二节点，就设置成正在处理
                    if (this.Root.Children.Count > 0 && this.Root.Children[0].Equals(a))
                    {
                        activity.Status = (int)ActivityProcessStatus.Processing;
                        ActivityModel activitymodel = new ActivityModel
                        {
                            Value = activity,
                        };
                        var idlist = iauth.GetUserIDList(authList);
                        var tasklist = activitymodel.GetTask(creator, idlist);
                        foreach (var task in tasklist)
                        {
                            taskdao.Add(task);
                        }
                    }
                    activitydao.Add(activity);
                    activityEntities.Add(activity);
                }

                foreach (var l in links)
                {
                    Link link = l.Value.ConvertTo<Link>();
                    link.Creator = creator;
                    link.WorkflowID = wf.ID;
                    link.ID = null;
                    link.LinkDefinitionID = l.Value.ID;
                    var fromactivity = activityEntities.Find(t => t.ActivityDefinitionID == l.Value.FromActivityDefinitionID);
                    var totactivity = activityEntities.Find(t => t.ActivityDefinitionID == l.Value.ToActivityDefinitionID);
                    if (fromactivity != null)
                    {
                        link.FromActivityID = fromactivity.ID;
                    }
                    if (totactivity != null)
                    {
                        link.ToAcivityID = totactivity.ID;
                    }
                    linkdao.Add(link);
                }
                mapper.CommitTransaction();

                model = WorkflowModel.Load(wf.ID);
                return model;
            }
            catch
            {
                mapper.RollBackTransaction();
                throw;
            }

        }

        public static WorkflowDefinitionModel Load(string id)
        {
            var mapper = Mapper.Instance();
            WorkflowDefinitionDao wfdao = new WorkflowDefinitionDao(mapper);
            ActivityDefinitionDao addao = new ActivityDefinitionDao(mapper);
            LinkDefinitionDao linkdao = new LinkDefinitionDao(mapper);
            ActivityAuthDefinitionDao aadd = new ActivityAuthDefinitionDao(mapper);
            WorkflowDefinition workflow = null;
            List<ActivityDefinition> activityList = new List<ActivityDefinition>();
            List<LinkDefinition> linkList = new List<LinkDefinition>();
            List<ActivityAuthDefinition> authList = new List<ActivityAuthDefinition>();
            WorkflowDefinitionModel model = null;
            var item = cache.GetItem(id);
            if (item != null)
            {
                model = item.Value as WorkflowDefinitionModel;
                return model;
            }
            try
            {

                mapper.BeginTransaction();
                workflow = wfdao.Query(new WorkflowDefinitionQueryForm { ID = id, Enabled = 1 }).FirstOrDefault();
                activityList = addao.Query(new ActivityDefinitionQueryForm { WorkflowDefinitionID = id, Enabled = 1 });
                linkList = linkdao.Query(new LinkDefinitionQueryForm { WorkflowDefinitionID = id });
                authList = aadd.Query(new ActivityAuthDefinitionQueryForm { WorkflowDefinitionID = id });
                mapper.CommitTransaction();
            }
            catch
            {
                throw;
            }
            if (workflow != null)
            {
                List<ActivityDefinitionModel> activityModelList = new List<ActivityDefinitionModel>();
                List<LinkDefinitionModel> linkModelList = new List<LinkDefinitionModel>();
                model = new WorkflowDefinitionModel
                {
                    value = workflow,
                };
                bool foundRoot = false;
                foreach (var link in linkList)
                {
                    linkModelList.Add(new LinkDefinitionModel
                    {
                        Value = link,
                    });
                }
                foreach (var activity in activityList)
                {
                    List<LinkDefinitionModel> preLinks = new List<LinkDefinitionModel>();
                    var preLinksTemp = linkModelList.FindAll(t => t.Value.ToActivityDefinitionID.Equals(activity.ID));

                    List<LinkDefinitionModel> nextLinks = new List<LinkDefinitionModel>();
                    var nextLinksTemp = linkModelList.FindAll(t => t.Value.FromActivityDefinitionID.Equals(activity.ID));


                    ActivityDefinitionModel activitymodel = new ActivityDefinitionModel
                    {
                        Value = activity,
                        AuthDefinition = authList.FindAll(t => t.ActivityDefinitionID == activity.ID),
                    };
                    activitymodel.PreLinks = preLinksTemp;
                    activitymodel.NextLinks = nextLinksTemp;
                    activityModelList.Add(activitymodel);
                    //没有父连接说明是根节点
                    if (!foundRoot && preLinksTemp.Count == 0)
                    {
                        model.Root = activitymodel;
                        foundRoot = true;
                    }
                }
                item = new CacheItem(id, model);
                cache.AddItem(item, 30 * 60);
            }

            return model;
        }

        public void Remove()
        {
            var mapper = Mapper.Instance();
            WorkflowDefinitionDao wfdao = new WorkflowDefinitionDao(mapper);
            ActivityDefinitionDao addao = new ActivityDefinitionDao(mapper);
            LinkDefinitionDao linkdao = new LinkDefinitionDao(mapper);
            ActivityAuthDefinitionDao aadd = new ActivityAuthDefinitionDao(mapper);
            try
            {
                mapper.BeginTransaction();
                string id = this.value.ID;
                wfdao.Delete(new WorkflowDefinitionQueryForm { ID = id });
                addao.Delete(new ActivityDefinitionQueryForm { WorkflowDefinitionID = id });
                linkdao.Delete(new LinkDefinitionQueryForm { WorkflowDefinitionID = id });
                aadd.Delete(new ActivityAuthDefinitionQueryForm { WorkflowDefinitionID = id });
                mapper.CommitTransaction();
            }
            catch
            {
                mapper.RollBackTransaction();
                throw;
            }
        }
    }
}
