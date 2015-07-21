using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class ContextUpdateForm : BaseUpdateForm<Context>
    {
        public ContextQueryForm ContextQueryForm { get; set; }
    }
}