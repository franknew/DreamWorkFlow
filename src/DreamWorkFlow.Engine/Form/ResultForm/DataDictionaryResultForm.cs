using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Form
{
    public class DataDictionaryResultForm
    {
        public DataDictionaryGroup Group { get; set; }

        public List<DataDictionary> Items { get; set; }
    }
}
