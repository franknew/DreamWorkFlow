using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOAFramework.Library.DAL;

namespace DreamWorkflow.Engine.Model
{
    public class User_Role : BaseEntity
    {
        public string UserID { get; set; }
        
        public string RoleID { get; set; }
        
    }
}