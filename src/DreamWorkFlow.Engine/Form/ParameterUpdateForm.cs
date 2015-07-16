using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class ParameterUpdateForm : BaseUpdateForm<DreamWorkflow.Engine.Model.Parameter>
    {
        public ParameterQueryForm ParameterQueryForm { get; set; }
    }
}