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
    public partial class UserDao : SimpleDao<User, UserQueryForm, UserUpdateForm>
    {
        public UserDao(ISqlMapper mapper)
            : base(mapper)
        {
        }
        
        public UserDao()
            : base(null)
        {
        }
        
        public DateTime QueryMaxLastUpdateTime()
        {
            return Mapper.QueryForObject<DateTime>("QueryUserLastUpdateTime", null);
        }
    }
}