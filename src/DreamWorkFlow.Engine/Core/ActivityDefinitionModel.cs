using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library;

namespace DreamWorkflow.Engine
{
    public class ActivityDefinitionModel : Node<ActivityDefinition>
    {
        private List<LinkDefinitionModel> preLinks = new List<LinkDefinitionModel>();

        public List<LinkDefinitionModel> PreLinks
        {
            get { return preLinks; }
            set 
            { 
                preLinks = value; 
                foreach (var link in value)
                {
                    link.ToActivityDefinition = this;
                }
            }
        }

        private List<LinkDefinitionModel> nextLinks = new List<LinkDefinitionModel>();

        public List<LinkDefinitionModel> NextLinks
        {
            get { return nextLinks; }
            set 
            { 
                nextLinks = value; 
                foreach (var link in value)
                {
                    link.FromActivityDefinition = this;
                }
            }
        }

        private List<ActivityAuthDefinition> authDefinition = new List<ActivityAuthDefinition>();

        public List<ActivityAuthDefinition> AuthDefinition
        {
            get { return authDefinition; }
            set { authDefinition = value; }
        }

        public bool Save(ISqlMapper mapper)
        {
            ActivityDefinitionDao dao = new ActivityDefinitionDao(mapper);
            var activity = dao.Query(new ActivityDefinitionQueryForm { ID = this.Value.ID }).FirstOrDefault();
            if (activity == null)
            {
                dao.Add(this.Value);
            }
            else
            {
                dao.Update(new ActivityDefinitionUpdateForm { Entity = this.Value, ActivityDefinitionQueryForm = new ActivityDefinitionQueryForm { ID = this.Value.ID } });
            }
            return true;
        }
    }
}
