using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.DAL
{
    public class WorkflowDao
    {
        private ISqlMapper mapper = null;

        public WorkflowDao(ISqlMapper mapper = null)
        {
            if (mapper == null)
            {
                this.mapper = Mapper.Instance();
            }
            else
            {
                this.mapper = mapper;
            }
        }


        public string Add(Workflow workflow)
        {
            if (string.IsNullOrEmpty(workflow.ID))
            {
                workflow.ID = Guid.NewGuid().ToString().Replace("-", "");
            }
            mapper.Insert("AddWorkflow", workflow);
            return workflow.ID;
        }

        public List<Workflow> Query(WorkflowQueryForm form)
        {
            return mapper.QueryForList<Workflow>("QueryWorkflow", form).ToList();
        }

        public bool Delete(WorkflowQueryForm form)
        {
            mapper.Delete("DeleteWorkflow", form);
            return true;
        }

        public bool Update(Workflow workflow)
        {
            mapper.Update("UpdateWorkflow", workflow);
            return true;
        }
    }
}
