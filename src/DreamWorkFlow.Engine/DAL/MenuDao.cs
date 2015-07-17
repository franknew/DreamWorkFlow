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
    public partial class MenuDao : SimpleDao<Menu, MenuQueryForm, MenuUpdateForm>
    {
        public MenuDao(ISqlMapper mapper)
            : base(mapper)
        {
        }
        
        public MenuDao()
            : base(null)
        {
        }
        
        public DateTime QueryMaxLastUpdateTime()
        {
            return Mapper.QueryForObject<DateTime>("QueryMenuLastUpdateTime", null);
        }
    }
}