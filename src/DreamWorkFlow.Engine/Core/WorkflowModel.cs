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
using SOAFramework.Library;

namespace DreamWorkflow.Engine
{
    public class WorkflowModel
    {
        private static ICache cache = CacheFactory.Create();

        #region property
        public ActivityModel Root { get; set; }

        public ActivityModel Tail { get; set; }

        public List<Approval> Approval { get; set; }

        private Workflow value = new Workflow();

        public Workflow Value
        {
            get { return this.value; }
            private set { this.value = value; }
        }

        public ActivityModel CurrentActivity
        {
            get
            {
                return Activities.Find(t => t.Value.Status.Value == (int)ActivityProcessStatus.Processing) as ActivityModel;
            }
        }

        public List<Node<Activity>> Activities
        {
            get
            {
                if (activities == null) activities = Root.GetList();
                return activities;
            }
        }

        private List<Node<Activity>> activities;
        #endregion

        #region action
        /// <summary>
        /// 从数据库读取流程信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WorkflowModel Load(string workflowid)
        {
            if (string.IsNullOrEmpty(workflowid)) return null;

            return Load(new List<string> { workflowid }).FirstOrDefault();
        }

        /// <summary>
        /// 从数据库读取流程信息
        /// </summary>
        /// <param name="workflowids"></param>
        /// <returns></returns>
        public static List<WorkflowModel> Load(WorkflowQueryForm form)
        {
            List<WorkflowModel> models = new List<WorkflowModel>();
            if (form == null) return models;

            #region query data
            ISqlMapper mapper = MapperHelper.GetMapper();
            WorkflowDao wfdao = new WorkflowDao(mapper);
            ActivityDao activitydao = new ActivityDao(mapper);
            LinkDao linkDao = new LinkDao(mapper);
            ActivityAuthDao authdao = new ActivityAuthDao(mapper);
            ApprovalDao approvalDao = new ApprovalDao(mapper);
            TaskDao taskdao = new TaskDao(mapper);
            List<Activity> activityList = new List<Activity>();
            List<Link> linkList = new List<Link>();
            List<ActivityAuth> authList = new List<ActivityAuth>();
            List<Approval> approvalList = new List<Approval>();
            List<Task> taskList = new List<Task>();
            List<Workflow> workflows = new List<Workflow>();
            //先从缓存取值
            //var item = cache.GetItem(id);
            //if (item != null)
            //{
            //    model = item.Value as WorkflowModel;
            //    return model;
            //}
            workflows = wfdao.Query(form);
            var workflowids = (from wf in workflows select wf.ID).ToList();
            activityList = activitydao.Query(new ActivityQueryForm { WorkflowIDs = workflowids });
            linkList = linkDao.Query(new LinkQueryForm { WorkflowIDs = workflowids });
            approvalList = approvalDao.Query(new ApprovalQueryForm { WorkflowIDs = workflowids });
            authList = authdao.Query(new ActivityAuthQueryForm { WorkflowIDs = workflowids });
            taskList = taskdao.Query(new TaskQueryForm { WorkflowIDs = workflowids });
            #endregion

            foreach (var workflow in workflows)
            {
                var model = BuildWorkflow(workflow, activityList, linkList, authList, approvalList, taskList);
                models.Add(model);
            }
            return models;
        }

        public static List<WorkflowModel> Load(List<string> workflowids)
        {
            List<WorkflowModel> models = new List<WorkflowModel>();
            if (workflowids == null || workflowids.Count == 0) return models;
            return Load(new WorkflowQueryForm { IDs = workflowids });
        }

        public static List<WorkflowModel> LoadByProcessID(List<string> processids)
        {
            List<WorkflowModel> models = new List<WorkflowModel>();
            if (processids == null || processids.Count == 0) return models;

            return Load(new WorkflowQueryForm { ProcessIDs = processids });
        }

        public static WorkflowModel LoadByProcessID(string processid)
        {
            if (string.IsNullOrEmpty(processid)) return null;

            return LoadByProcessID(new List<string>() { processid }).FirstOrDefault();
        }

        /// <summary>
        /// 把流程实体新增到数据库
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            ISqlMapper mapper = MapperHelper.GetMapper();
            WorkflowDao workflowdao = new WorkflowDao(mapper);
            ActivityDao activitydao = new ActivityDao(mapper);
            LinkDao linkdao = new LinkDao(mapper);
            ActivityAuthDao authdao = new ActivityAuthDao(mapper);
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
            return true;
        }

        /// <summary>
        /// 处理流程流转：审批，或者直接走到下一步
        /// </summary>
        /// <param name="activityid"></param>
        /// <param name="approval"></param>
        /// <param name="taskid"></param>
        /// <param name="processor"></param>
        /// <param name="auth"></param>
        public void ProcessActivity(Approval approval, string processor, IWorkflowAuthority auth)
        {
            var activities = this.Root.GetList();
            //MonitorCache.GetInstance().PushMessage(new CacheMessage { Message = "activity id:" + activityid }, CacheEnum.FormMonitor);
            if (CurrentActivity == null) throw new Exception("没有当前运行的Activity");
            bool isTail = CurrentActivity == this.Tail;
            CurrentActivity.Process(approval, processor, auth);
            //如果是尾节点，就设置流程结束
            if (isTail) FinishWorkflow(processor);
            else ContinueWorkflow(processor);
        }

        /// <summary>
        /// 结束流程
        /// </summary>
        /// <param name="lastupdator"></param>
        public void FinishWorkflow(string lastupdator)
        {
            ISqlMapper mapper = MapperHelper.GetMapper();
            WorkflowDao workflowdao = new WorkflowDao(mapper);
            this.value.LastUpdator = lastupdator;
            this.value.Status = (int)WorkflowProcessStatus.Processed;
            workflowdao.Update(new WorkflowUpdateForm
            {
                Entity = new Workflow
                {
                    Status = this.value.Status,
                    LastUpdator = this.value.LastUpdator,
                },
                WorkflowQueryForm = new WorkflowQueryForm { ID = this.value.ID }
            });
        }

        /// <summary>
        /// 继续流程
        /// </summary>
        /// <param name="lastupdator"></param>
        public void ContinueWorkflow(string lastupdator)
        {
            ISqlMapper mapper = MapperHelper.GetMapper();
            WorkflowDao workflowdao = new WorkflowDao(mapper);
            this.value.LastUpdator = lastupdator;
            this.value.Status = (int)WorkflowProcessStatus.Processing;
            workflowdao.Update(new WorkflowUpdateForm
            {
                Entity = new Workflow
                {
                    Status = this.value.Status,
                    LastUpdator = this.value.LastUpdator,
                },
                WorkflowQueryForm = new WorkflowQueryForm { ID = this.value.ID }
            });
        }

        /// <summary>
        /// 作废(停止)流程
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="processor"></param>
        public void Stop(string processor)
        {
            if (this.CurrentActivity != null)
            {
                ISqlMapper mapper = MapperHelper.GetMapper();
                WorkflowDao wfdao = new WorkflowDao(mapper);
                TaskDao taskdao = new TaskDao(mapper);
                this.value.Status = (int)WorkflowProcessStatus.Stoped;
                this.value.LastUpdator = processor;
                wfdao.Update(new WorkflowUpdateForm
                {
                    Entity = new Workflow
                    {
                        LastUpdator = this.value.LastUpdator,
                        Status = this.value.Status,
                    },
                    WorkflowQueryForm = new WorkflowQueryForm
                    {
                        ID = this.value.ID,
                    }
                });
                taskdao.Update(new TaskUpdateForm
                {
                    Entity = new Task { Status = (int)TaskProcessStatus.Stoped },
                    TaskQueryForm = new TaskQueryForm { ActivityID = this.CurrentActivity.Value.ID }
                });
            }
        }

        /// <summary>
        /// 删除该流程
        /// </summary>
        public void Remove()
        {
            ISqlMapper mapper = MapperHelper.GetMapper();
            WorkflowDao wfdao = new WorkflowDao(mapper);
            ActivityDao activitydao = new ActivityDao(mapper);
            LinkDao linkDao = new LinkDao(mapper);
            ActivityAuthDao authdao = new ActivityAuthDao(mapper);
            ApprovalDao approvalDao = new ApprovalDao(mapper);
            TaskDao taskdao = new TaskDao(mapper);
            string id = this.value.ID;
            var activities = activitydao.Query(new ActivityQueryForm { WorkflowID = id });
            taskdao.Delete(new TaskQueryForm { WorkflowID = id });
            wfdao.Delete(new WorkflowQueryForm { ID = id });
            activitydao.Delete(new ActivityQueryForm { WorkflowID = id });
            linkDao.Delete(new LinkQueryForm { WorkflowID = id });
            approvalDao.Delete(new ApprovalQueryForm { WorkflowID = id });
            authdao.Delete(new ActivityAuthQueryForm { WorkflowID = id });

        }

        /// <summary>
        /// 打开任务
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="proccessor"></param>
        public void ReadTask(string taskid, string proccessor)
        {
            var activity = this.Root.GetList().Find(t => t.Value.ID == taskid) as ActivityModel;
            activity.ReadTask(taskid, proccessor);
        }

        /// <summary>
        /// 用户是否有任务处理
        /// </summary>
        /// <param name="processor"></param>
        /// <returns></returns>
        public bool CanUserProcess(string processor)
        {
            if (CurrentActivity == null) return false;
            return CurrentActivity.CanUserProcess(processor);
        }

        /// <summary>
        /// 获得用户正在处理的任务
        /// </summary>
        /// <param name="processor"></param>
        /// <returns></returns>
        public Task GetUserProcessingTask(string processor)
        {
            if (CurrentActivity == null) return null;
            return CurrentActivity.GetUserProcessingTask(processor);
        }
        #endregion

        #region private 
        private static WorkflowModel BuildWorkflow(Workflow workflow, List<Activity> activities, List<Link> links,
            List<ActivityAuth> authes, List<Approval> approvals, List<Task> tasks)
        {
            if (workflow == null) throw new Exception("workflow不能为null");
            List<ActivityModel> activityInstanceList = new List<ActivityModel>();
            List<LinkModel> linkModelList = new List<LinkModel>();
            var activityList = activities.FindAll(t => t.WorkflowID.Equals(workflow.ID));
            var linkList = links.FindAll(t => t.WorkflowID.Equals(workflow.ID));
            var authList = authes.FindAll(t => t.WorkflowID.Equals(workflow.ID));
            var approvalList = approvals.FindAll(t => t.WorkflowID.Equals(workflow.ID));
            var taskList = tasks.FindAll(t => t.WorkflowID.Equals(workflow.ID));
            WorkflowModel model = new WorkflowModel
            {
                value = workflow,
                Approval = approvalList,
            };
            bool foundRoot = false;
            bool foundTail = false;
            foreach (var link in linkList)
            {
                linkModelList.Add(new LinkModel
                {
                    Value = link,
                });
            }
            foreach (var activity in activityList)
            {
                //pre link
                var preLink = linkModelList.FindAll(t => t.Value != null && t.Value.ToAcivityID == activity.ID);
                //next link
                var nextLink = linkModelList.FindAll(t => t.Value != null && t.Value.FromActivityID == activity.ID);

                var activityInstance = new ActivityModel
                {
                    Value = activity,
                    Approvals = model.Approval.FindAll(t => t.ActivityID == activity.ID),
                    PreLinks = preLink,
                    NextLinks = nextLink,
                    Tasks = tasks.FindAll(t => t.ActivityID == activity.ID),
                    OwnerWorkflow = model,
                };

                //处理权限
                var activityauth = authList.FindAll(t => t.ActivityID == activity.ID);
                activityInstance.Auth.AddRange(activityauth);
                //给审批排序
                activityInstance.Approvals.Sort((l, r) =>
                {
                    if (l.CreateTime > l.CreateTime) return 1;
                    else return 0;
                });
                if (!foundRoot && preLink.Count == 0)
                {
                    model.Root = activityInstance;
                    foundRoot = true;
                }
                if (!foundTail && nextLink.Count == 0)
                {
                    model.Tail = activityInstance;
                    foundTail = true;
                }
                activityInstanceList.Add(activityInstance);
            }
            return model;
        }
        #endregion

    }
}