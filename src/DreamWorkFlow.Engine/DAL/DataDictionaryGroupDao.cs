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
    public partial class DataDictionaryGroupDao : SimpleDao<DataDictionaryGroup, DataDictionaryGroupQueryForm, DataDictionaryGroupUpdateForm>
    {
        public DataDictionaryGroupDao(ISqlMapper mapper)
            : base(mapper)
        {
        }
        
        public DataDictionaryGroupDao()
            : base(null)
        {
        }
        
        public DateTime QueryMaxLastUpdateTime()
        {
            return Mapper.QueryForObject<DateTime>("QueryDataDictionaryGroupLastUpdateTime", null);
        }
    }
}