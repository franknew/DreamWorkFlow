using DreamWorkflow.Engine.DAL;
using DreamWorkflow.Engine.Form;
using DreamWorkflow.Engine.Model;
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
            List<DataDictionaryResultForm> result = new List<DataDictionaryResultForm>();
            DataDictionaryGroupAndItemDao dao = new DataDictionaryGroupAndItemDao();
            var list = dao.QueryByGroupName(nameList);
            foreach (var item in list)
            {
                var form = result.Find(t => t.Group.ID == item.GroupID);
                if (form == null)
                {
                    form = new DataDictionaryResultForm
                    {
                        Group = new DataDictionaryGroup
                        {
                            ID = item.GroupID,
                            Name = item.GroupName,
                        },
                        Items = new List<DataDictionary>(),
                    };
                    result.Add(form);
                }
                form.Items.Add(new DataDictionary
                {
                    ID = item.DataDictionaryID,
                    Name = item.DataDictionaryName,
                    Value = item.DataDictionaryValue,
                });
            }
            return result;
        }
    }
}
