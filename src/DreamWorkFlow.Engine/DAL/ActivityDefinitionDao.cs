using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.DAL
{
    public class ActivityDefinitionDao
    {
        private ISqlMapper mapper = null;

        public ActivityDefinitionDao(ISqlMapper mapper = null)
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

        public string Add(WorkflowDefinition workflow)
        {
            if (string.IsNullOrEmpty(workflow.ID))
            {
                workflow.ID = Guid.NewGuid().ToString().Replace("-", "");
            }
            mapper.Insert("AddWorkflowDefinition", workflow);
            return workflow.ID;
        }

        public List<WorkflowDefinition> Query(WorkflowDefinitionQueryForm form)
        {
            return mapper.QueryForList<WorkflowDefinition>("QueryWorkflowDefinition", form).ToList();
        }

        public bool Delete(WorkflowDefinitionQueryForm form)
        {
            mapper.Delete("DeleteWorkflowDefinition", form);
            return true;
        }

        public bool Update(WorkflowDefinition workflow)
        {
            mapper.Update("UpdateWorkflowDefinition", workflow);
            return true;
        }
    }
}
