﻿using DreamWorkflow.Engine.Model;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.DAL
{

    public class BaseDao<TEngity, TQueryForm> where TEngity : BaseEntity
    {

        private ISqlMapper mapper = null;

        private string tableName = null;

        public BaseDao(ISqlMapper mapper = null)
        {
            if (mapper == null)
            {
                this.mapper = Mapper.Instance();
            }
            else
            {
                this.mapper = mapper;
            }
            tableName = typeof(TEngity).Name;
        }

        public string Add(TEngity entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
            {
                entity.ID = Guid.NewGuid().ToString().Replace("-", "");
            }
            mapper.Insert("Add" + tableName, entity);
            return entity.ID;
        }

        public List<TEngity> Query(TQueryForm form)
        {
            return mapper.QueryForList<TEngity>("Query" + tableName, form).ToList();
        }

        public bool Delete(TQueryForm form)
        {
            mapper.Delete("Delete" + tableName, form);
            return true;
        }

        public bool Update(TEngity workflow)
        {
            mapper.Update("Update" + tableName, workflow);
            return true;
        }
    }
}