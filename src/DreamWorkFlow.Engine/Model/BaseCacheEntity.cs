using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Model
{
    public class CacheEntity<TEntity>
    {
        public List<TEntity> List { get; set; }

        public DateTime? LastUpdateTime { get; set; }
    }
}
