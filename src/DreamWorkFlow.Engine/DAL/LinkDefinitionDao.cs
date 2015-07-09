using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.DAL
{
    public class LinkDefinitionDao : BaseDao<LinkDefinition, LinkDefinitionQueryForm, LinkDefinitionUpdateForm>
    {
        public LinkDefinitionDao(ISqlMapper mapper = null)
            : base(mapper)
        {

        }
    }
}