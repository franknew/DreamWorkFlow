using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
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
        public static List<TEntity> GetDataFromCache<TEntity>(Type daoType) where TEntity: SimpleEntity
        {
            string key = daoType.FullName;
            ICache cache = CacheFactory.Create(CacheType.DefaultMemoryCache);
            var item = cache.GetItem(key);
            List<TEntity> list = null;
            if (item == null)
            {
                object dao = Activator.CreateInstance(daoType, null);
                var method = daoType.GetMethod("Query");
                object result = method.Invoke(dao, new object[] { null });
                list = result as List<TEntity>;
                //item = new CacheItem(key, )
            }
            else
            {

            }
            return list;
        }
    }
}
