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
    public class ActivityDefinitionInstance : Node<ActivityDefinition>
    {

        private ActivityAuthDefinition value = new ActivityAuthDefinition();

        public ActivityAuthDefinition Value
        {
            get { return this.value; }
            private set { this.value = value; }
        }

        private List<LinkDefinition> preLinks = new List<LinkDefinition>();

        public List<LinkDefinition> PreLinks
        {
            get { return preLinks; }
            set { preLinks = value; }
        }

        private List<LinkDefinition> nextLinks = new List<LinkDefinition>();

        public List<LinkDefinition> NextLinks
        {
            get { return nextLinks; }
            set { nextLinks = value; }
        }

        
    }
}
