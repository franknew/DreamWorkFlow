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
    public partial class ActivityAuthDefinitionDao : SimpleDao<ActivityAuthDefinition, ActivityAuthDefinitionQueryForm, ActivityAuthDefinitionUpdateForm>
    {
        public ActivityAuthDefinitionDao(ISqlMapper mapper)
            : base(mapper)
        {
        }
        
        public ActivityAuthDefinitionDao()
            : base(null)
        {
        }
        
        public DateTime QueryMaxLastUpdateTime()
        {
            return Mapper.QueryForObject<DateTime>("QueryActivityAuthDefinitionLastUpdateTime", null);
        }
    }
}