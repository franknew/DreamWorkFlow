using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.DAL
{
    public partial class LinkDefinitionDao : SimpleDao<LinkDefinition, LinkDefinitionQueryForm, LinkDefinitionUpdateForm>
    {
        public LinkDefinitionDao(ISqlMapper mapper = null)
            : base(mapper)
        {

        }
    }
}