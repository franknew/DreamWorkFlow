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
    public partial class Role_Module_ActionDao : BaseDao<Role_Module_Action, Role_Module_ActionQueryForm, Role_Module_ActionUpdateForm>
    {
        public Role_Module_ActionDao(ISqlMapper mapper)
            : base(mapper)
        {
        }
        
        public Role_Module_ActionDao()
            : base(null)
        {
        }
        
    }
}