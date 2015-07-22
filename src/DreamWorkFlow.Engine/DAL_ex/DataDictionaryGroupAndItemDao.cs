using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.DAL
{
    public class DataDictionaryGroupAndItemDao
    {
        private ISqlMapper mapper = null;

        public ISqlMapper Mapper { get { return mapper; } }

        public DataDictionaryGroupAndItemDao(ISqlMapper mapper = null)
        {
            if (mapper != null)
            {
                this.mapper = mapper;
            }
            else
            {
                this.mapper = IBatisNet.DataMapper.Mapper.Instance();
            }
        }


        public List<DataDictionaryGroupAndItem> QueryByGroupName(List<string> groupName)
        {
            return mapper.QueryForList<DataDictionaryGroupAndItem>("QueryDataDictionaryGroupAndItemByGroupName", groupName).ToList();
        }
    }
}
