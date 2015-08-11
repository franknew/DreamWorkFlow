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
    public class LinkDefinitionModel
    {
        private LinkDefinition value = new LinkDefinition();

        public LinkDefinition Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private ActivityDefinitionModel fromActivityDefinition = null;
        public ActivityDefinitionModel FromActivityDefinition
        {
            get
            {
                return fromActivityDefinition;
            }
            set
            {
                fromActivityDefinition = value;
                if (!fromActivityDefinition.NextLinks.Contains(this))
                {
                    fromActivityDefinition.NextLinks.Add(this);
                }
                if (toActivityDefinition != null)
                {
                    if (!fromActivityDefinition.Children.Contains(toActivityDefinition))
                    {
                        fromActivityDefinition.Children.Add(toActivityDefinition);
                    }
                    if (!toActivityDefinition.Parents.Contains(value))
                    {
                        toActivityDefinition.Parents.Add(value);
                    }
                }
            }
        }

        private ActivityDefinitionModel toActivityDefinition = null;
        public ActivityDefinitionModel ToActivityDefinition
        {
            get { return toActivityDefinition; }
            set
            {
                toActivityDefinition = value;
                if (!toActivityDefinition.PreLinks.Contains(this))
                {
                    toActivityDefinition.PreLinks.Add(this);
                }
                if (fromActivityDefinition != null)
                {
                    if (!fromActivityDefinition.Children.Contains(value))
                    {
                        fromActivityDefinition.Children.Add(value);
                    }
                    if (!toActivityDefinition.Parents.Contains(fromActivityDefinition))
                    {
                        toActivityDefinition.Parents.Add(fromActivityDefinition);
                    }
                }
            }
        }

        public bool Save(ISqlMapper mapper)
        {
            LinkDefinitionDao dao = new LinkDefinitionDao(mapper);
            var link = dao.Query(new LinkDefinitionQueryForm { ID = this.value.ID }).FirstOrDefault();
            if (link == null)
            {
                dao.Add(this.value);
            }
            else
            {
                dao.Update(new LinkDefinitionUpdateForm { Entity = this.value, LinkDefinitionQueryForm = new LinkDefinitionQueryForm { ID = this.value.ID } });
            }
            return true;
        }
    }
}
