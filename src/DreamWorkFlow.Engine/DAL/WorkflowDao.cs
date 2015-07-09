using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.DAL
{
    public class WorkflowDao : BaseDao<Workflow, WorkflowQueryForm, WorkflowUpdateForm>
    {
        public WorkflowDao(ISqlMapper mapper = null)
            : base(mapper)
        {

        }
    }
}