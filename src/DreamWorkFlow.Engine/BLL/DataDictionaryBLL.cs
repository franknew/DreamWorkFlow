using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using SOAFramework.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine
{
    public class DataDictionaryBLL
    {
        public List<DataDictionaryResultForm> QueryByGroupName(List<string> nameList)
        {
            List<DataDictionaryResultForm> list = new List<DataDictionaryResultForm>();
            if (nameList == null)
            {
                return list;
            }
            var datadiclist = TableCacheHelper.GetDataFromCache<DataDictionary>(typeof(DataDictionaryDao));
            var datadicgrouplist = TableCacheHelper.GetDataFromCache<DataDictionaryGroup>(typeof(DataDictionaryGroupDao));
            foreach (var name in nameList)
            {
                var group = datadicgrouplist.Find(t => t.Name.Equals(name));
                if (group == null)
                {
                    continue;
                }
                var datadic = datadiclist.FindAll(t => t.DataDictionaryGroupID == group.ID);
                DataDictionaryResultForm dicform = new DataDictionaryResultForm
                {
                    Group = group,
                    Items = datadic,
                };
                list.Add(dicform);
            }
            return list;
        }

        /// <summary>
        /// 查询所有数据字典
        /// </summary>
        /// <returns></returns>
        public List<DataDictionaryResultForm> QueryAll()
        {
            var datadiclist = TableCacheHelper.GetDataFromCache<DataDictionary>(typeof(DataDictionaryDao));
            var datadicgrouplist = TableCacheHelper.GetDataFromCache<DataDictionaryGroup>(typeof(DataDictionaryGroupDao));
            List<DataDictionaryResultForm> list = new List<DataDictionaryResultForm>();
            foreach (var group in datadicgrouplist)
            {
                var datadic = datadiclist.FindAll(t => t.DataDictionaryGroupID == group.ID);
                DataDictionaryResultForm dicform = new DataDictionaryResultForm
                {
                    Group = group,
                    Items = datadic,
                };
                list.Add(dicform);
            }
            return list;
        }

        /// <summary>
        /// 新增数据字典组
        /// </summary>
        /// <param name="group"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public string AddGroup(DataDictionaryGroup group)
        {
            if (group == null)
            {
                throw new Exception("数据字典分组不能为null");
            }
            ISqlMapper mapper = MapperHelper.GetMapper();
            DataDictionaryGroupDao groupdao = new DataDictionaryGroupDao(mapper);
            string id =  groupdao.Add(group);
            return id;
        }

        /// <summary>
        /// 新增数据字典明细
        /// </summary>
        /// <param name="items"></param>
        /// <returns>返回数据组回传加上了id</returns>
        public List<DataDictionary> AddItems(List<DataDictionary> items)
        {
            ISqlMapper mapper = MapperHelper.GetMapper();
            DataDictionaryDao dicdao = new DataDictionaryDao(mapper);
            if (items != null)
            {
                foreach (var item in items)
                {
                    dicdao.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 更新数据字典
        /// </summary>
        /// <param name="group"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public List<DataDictionary> Update(DataDictionaryGroup group, List<DataDictionary> items)
        {
            if (group == null)
            {
                throw new Exception("数据字典分组不能为null");
            }
            ISqlMapper mapper = MapperHelper.GetMapper();
            DataDictionaryGroupDao groupdao = new DataDictionaryGroupDao(mapper);
            DataDictionaryDao dicdao = new DataDictionaryDao(mapper);
            groupdao.Update(new DataDictionaryGroupUpdateForm { Entity = group,
            DataDictionaryGroupQueryForm = new DataDictionaryGroupQueryForm
            {
                ID = group.ID,
            }
            });
            if (items != null)
            {
                foreach (var item in items)
                {
                    if (string.IsNullOrEmpty(item.ID))
                    {
                        dicdao.Add(item);
                    }
                    else
                    {
                        var dic = dicdao.Query(new DataDictionaryQueryForm { ID = item.ID });
                        if (dic == null)
                        {
                            dicdao.Add(item);
                        }
                        else
                        {
                            dicdao.Update(new DataDictionaryUpdateForm
                            {
                                Entity = item,
                                DataDictionaryQueryForm = new DataDictionaryQueryForm
                                {
                                    ID = item.ID,
                                }
                            });
                        }
                    }
                }
            }
            return items;
        }

        public bool Delete(string groupid)
        {
            ISqlMapper mapper = MapperHelper.GetMapper();
            DataDictionaryGroupDao groupdao = new DataDictionaryGroupDao(mapper);
            DataDictionaryDao dicdao = new DataDictionaryDao(mapper);
            groupdao.Delete(new DataDictionaryGroupQueryForm { ID = groupid });
            dicdao.Delete(new DataDictionaryQueryForm { DataDictionaryGroupID = groupid });
            return true;
        }

        public bool DeleteItem(string itemid)
        {
            ISqlMapper mapper = MapperHelper.GetMapper();
            DataDictionaryDao dicdao = new DataDictionaryDao(mapper);
            dicdao.Delete(new DataDictionaryQueryForm { ID = itemid });
            return true;
        }
    }
}
