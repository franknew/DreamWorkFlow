using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamWorkflow.Engine.Model;

namespace DreamWorkflow.Engine
{
    public class LinkModel
    {

        private ActivityModel fromActivity = null;

        public ActivityModel FromActivity
        {
            get { return fromActivity; }
            set 
            { 
                fromActivity = value;
                if (!fromActivity.NextLinks.Contains(this))
                {
                    fromActivity.NextLinks.Add(this);
                }
                if (toActivity != null)
                {
                    if (!fromActivity.Children.Contains(toActivity))
                    {
                        fromActivity.Children.Add(toActivity);
                    }
                    if (!toActivity.Parents.Contains(value))
                    {
                        toActivity.Parents.Add(value);
                    }
                }
            }
        }

        private ActivityModel toActivity = null;

        public ActivityModel ToActivity
        {
            get { return toActivity; }
            set 
            { 
                toActivity = value;
                if (!toActivity.PreLinks.Contains(this))
                {
                    toActivity.PreLinks.Add(this);
                }
                if (fromActivity != null)
                {
                    if (!fromActivity.Children.Contains(value))
                    {
                        fromActivity.Children.Add(value);
                    }
                    if (!toActivity.Parents.Contains(fromActivity))
                    {
                        toActivity.Parents.Add(fromActivity);
                    }
                }
            }
        }

        public Link Value { get; set; }
    }
}
