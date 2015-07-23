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
    public partial class RoleDao : SimpleDao<Role, RoleQueryForm, RoleUpdateForm>
    {
        public RoleDao(ISqlMapper mapper)
            : base(mapper)
        {
        }
        
        public RoleDao()
            : base(null)
        {
        }
        
        public DateTime QueryMaxLastUpdateTime()
        {
            return Mapper.QueryForObject<DateTime>("QueryRoleLastUpdateTime", null);
        }
    }
}