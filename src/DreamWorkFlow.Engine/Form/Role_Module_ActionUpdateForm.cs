using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class Role_Module_ActionUpdateForm : BaseUpdateForm<Role_Module_Action>
    {
        public Role_Module_ActionQueryForm Role_Module_ActionQueryForm { get; set; }
    }
}