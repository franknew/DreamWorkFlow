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

        public void Load(string id = null)
        {
            if (!string.IsNullOrEmpty(id))
            {
                this.value.ID = id;
            }
            WorkflowDao wfdao = new WorkflowDao();
            ActivityDao activitydao = new ActivityDao();
            LinkDao linkDao = new LinkDao();
            ApprovalDao approvalDao = new ApprovalDao();
            var workflowlist = wfdao.Query(new WorkflowQueryForm { ID = this.value.ID });
            var activitylist = activitydao.Query(new ActivityQueryForm { WorkflowID = this.value.ID });
            var linkList = linkDao.Query(new LinkQueryForm { WorkflowID = this.value.ID });
            Approval = approvalDao.Query(new ApprovalQueryForm { WorkflowID = this.value.ID });
            List<ActivityInstance> activityInstanceList = new List<ActivityInstance>();
            foreach (var activity in activitylist)
            {
                var toLink = linkList.FindAll(t => t.ToAcivityID == activity.ID);
                var toActivity = new ActivityInstance
                {
                    Value = activity,
                    Approvals = Approval.FindAll(t => t.ActivityID == activity.ID),
                    
                };

                if (toLink == null)
                {
                    this.Root = toActivity;

                }
                activityInstanceList.Add(toActivity);
            }
        }

        public void Save()
        {

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
