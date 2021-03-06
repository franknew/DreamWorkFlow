﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamWorkflow.Engine
{
    public class Node<T> where T : class
    {
        private List<Node<T>> children = new List<Node<T>>();

        public List<Node<T>> Children
        {
            get { return children; }
            set { children = value; }
        }

        private List<Node<T>> parents = new List<Node<T>>();

        public List<Node<T>> Parents
        {
            get { return parents; }
            set { parents = value; }
        }

        public List<Node<T>> GetList()
        {
            List<Node<T>> list = new List<Node<T>>();
            list.Add(this);
            RescGetList(this, list);
            return list;
        }

        public T Value { get; set; }

        private void RescGetList(Node<T> node, List<Node<T>> list)
        {
            if (node == null || node.children.Count == 0)
            {
                return;
            }
            foreach (var data in node.children)
            {
                list.Add(data);
                RescGetList(data, list);
            }
        }
    }
}
