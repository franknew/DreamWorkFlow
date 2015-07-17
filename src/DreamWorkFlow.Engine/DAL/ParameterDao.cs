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
    public partial class ParameterDao : BaseDao<DreamWorkflow.Engine.Model.Parameter, ParameterQueryForm, ParameterUpdateForm>
    {
        public ParameterDao(ISqlMapper mapper)
            : base(mapper)
        {
        }
        
        public ParameterDao()
            : base(null)
        {
        }
        
    }
}