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
            var workflowlist = wfdao.Query(new WorkflowQueryForm { ID = this.value.ID });
            var activitylist = activitydao.Query(new ActivityQueryForm { WorkflowID = this.value.ID });
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
