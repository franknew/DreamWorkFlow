using DreamWorkflow.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DreamWorkflow.Engine.Form
{
    public class TaskUpdateForm
    {
        public Task Task { get; set; }

        public TaskQueryForm TaskQueryForm { get; set; }
    }
}