using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Form
{
    public class User_RoleUpdateForm : BaseUpdateForm<User_Role>
    {
        public User_RoleQueryForm User_RoleQueryForm { get; set; }
    }
}