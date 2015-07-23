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
    public partial class User_PostDao : BaseDao<User_Post, User_PostQueryForm, User_PostUpdateForm>
    {
        public User_PostDao(ISqlMapper mapper)
            : base(mapper)
        {
        }
        
        public User_PostDao()
            : base(null)
        {
        }
        
    }
}