using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
using SOAFramework.Library.Cache;
using SOAFramework.Library.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace DreamWorkflow.Engine
{
    public class TableCacheHelper
    {
        public static List<TEntity> GetDataFromCache<TEntity>(Type daoType)
            where TEntity : SimpleEntity
        {
            string key = typeof(TEntity).FullName;
            ICache cache = CacheFactory.Create();
            var item = cache.GetItem(key);
            List<TEntity> list = null;
            if (item == null)
            {
                list = Query<TEntity>(daoType);
                if (list.Count == 0)
                {
                    return list;
                }
                CacheEntity<TEntity> cacheentity = new CacheEntity<TEntity>();
                cacheentity.List = list;
                cacheentity.LastUpdateTime = list[0].LastUpdateTime;
                foreach (var data in list)
                {
                    if (cacheentity.LastUpdateTime < list[0].LastUpdateTime)
                    {
                        cacheentity.LastUpdateTime = list[0].LastUpdateTime;
                    }
                }
                item = new CacheItem(key, cacheentity);
                cache.AddItem(item, 60 * 30);
            }
            else
            {
                object dao = Activator.CreateInstance(daoType, null);
                string tableName = typeof(TEntity).Name;
                var method = daoType.GetMethod("QueryMaxLastUpdateTime");
                object result = method.Invoke(dao, null);
                CacheEntity<TEntity> cacheentity = item.Value as CacheEntity<TEntity>;
                DateTime? lastupdatetime = result as DateTime?;
                if (cacheentity.LastUpdateTime < lastupdatetime)
                {
                    list = Query<TEntity>(daoType);
                    cacheentity.LastUpdateTime = lastupdatetime;
                    item.Value = cacheentity;
                    cache.UpdateItem(item);
                }
                else
                {
                    list = cacheentity.List;
                }
            }
            return list;
        }

        private static List<T> Query<T>(Type daoType)
        {
            object dao = Activator.CreateInstance(daoType, null);
            var method = daoType.GetMethod("Query");
            object result = method.Invoke(dao, new object[] { null });
            List<T> list = result as List<T>;
            return list;
        }
    }
}
