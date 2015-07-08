using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine.Model
{
    public class Link : BaseEntity
    {

        public string FromAcitivityID { get; set; }

        public string ToActivityID { get; set; }
    }
}
