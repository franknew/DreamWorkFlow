using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class Menu_RoleUpdateForm : BaseUpdateForm<Menu_Role>
    {
        public Menu_RoleQueryForm Menu_RoleQueryForm { get; set; }
    }
}