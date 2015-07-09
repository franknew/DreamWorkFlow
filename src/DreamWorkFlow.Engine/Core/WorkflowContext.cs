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
    public class WorkflowContext
    {
        public ActivityNode Root { get; set; }

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
            WorkflowDao dao = new WorkflowDao();
            var list = dao.Query(new WorkflowQueryForm { ID = this.value.ID });
        }

        public ActivityNode CurrentActivity
        {
            get
            {
                return null;
            }
        }
    }
}
