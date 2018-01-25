using SoInc.ModdingTool.Logic.Data;
using Syncfusion.Windows.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoInc.ModdingTool.UI.Code
{
    public class FeatureDiagramManager
    {
        /// <summary>
        /// represents a list of all elements
        /// </summary>
        public enum Elements
        {
            Node,
            Connector,
            Childs
        }

        /// <summary>
        /// Gets or Sets the Model of the Diagram
        /// </summary>
        public DiagramModel Model { get; private set; }

        /// <summary>
        /// Gets or sets the View of the Diagram
        /// </summary>
        public DiagramView View { get; private set; }

        /// <summary>
        /// Internal field for the features
        /// </summary>
        private List<Feature> features;

        /// <summary>
        /// Gets or sets the Features of the Diagram
        /// </summary>
        public List<Feature> Features
        {
            get
            {
                if (features == null)
                {
                    features = new List<Feature>();
                }
                return features;
            }
            set { features = value; }
        }

        /// <summary>
        /// Initialise the Manager
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="dm"></param>
        public FeatureDiagramManager(DiagramView dv, DiagramModel dm)
        {
            this.View = dv;
            this.Model = dm;
        }

        /// <summary>
        /// updates the Diagram completely
        /// </summary>
        /// <param name="advanced"></param>
        public void UpdateDiagram(bool advanced = false)
        {
            if (advanced)
            {
                Model.Connections.Clear();
                Model.Nodes.Clear();
                CreateDiagram();
            }

            var layout = new HierarchicalTreeLayout(Model, View);
            layout.RefreshLayout();
        }

        /// <summary>
        /// creates the whole Diagram
        /// </summary>
        protected void CreateDiagram()
        {
            var start = Features.FindAll(f => string.IsNullOrEmpty(f.From));
            foreach (var f in start)
            {
                CreateNode(f);
            }
        }

        /// <summary>
        /// creates a new node and sets the Connection if its required
        /// </summary>
        /// <param name="text"></param>
        /// <param name="parent"></param>
        public void CreateNode(string text, Node parent = null)
        {
            var node = new Node
            {
                Shape = Shapes.Rectangle,
                Width = 150,
                Height = 75,
                Label = text
            };

            Model.Nodes.Add(node);

            if (parent != null)
            {
                CreateConnector(parent, node);
            }
        }

        /// <summary>
        /// Creates the Child Nodes of the Start node recursively
        /// </summary>
        /// <param name="start"></param>
        public void CreateNode(Feature start)
        {
            Node parent = null;
            if (!string.IsNullOrEmpty(start.From))
            {
                parent = FindNode(start.From);
            }

            CreateNode(start.Name, parent);
            var startNode = FindNode(start.Name);

            var items = Features.FindAll(f => f.From == start.Name);

            foreach (var i in items)
            {
                CreateNode(i);
            }
        }

        /// <summary>
        /// Creates a LineCOnnector
        /// </summary>
        public void CreateConnector(Node start, Node end)
        {
            var conn = new LineConnector(start, end, View);
            Model.Connections.Add(conn);
        }

        /// <summary>
        /// deletes the Node, every connnection item with this node and all ChildItems if its recommended
        /// </summary>
        /// <param name="node"></param>
        /// <param name="deleteChilds">Should the child-items also be deleted?</param>
        public void DeleteNode(Node node, Elements element)
        {
            List<Node> nodes;
            List<LineConnector> connectors = new List<LineConnector>();
            Node currentNode = node;

            nodes = FindChilds(node, true);
            nodes.Add(node);

            //delete every Connector that are connected with the Nodes from the List
            foreach (var n in nodes)
            {
                var conn = FindConnectors(n);
                foreach(var c in conn)
                {
                    Model.Connections.Remove(c);
                }
            }

            switch (element)
            {
                case Elements.Node:
                    Model.Nodes.Remove(node);
                    break;
                case Elements.Childs:
                    //delete every connection in the list
                    foreach (var n in nodes)
                    {
                        Model.Nodes.Remove(n);
                    }
                    break;
                case Elements.Connector:
                default:
                    break;
            }
        }

        /// <summary>
        /// gets the first Node with the correct text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Node FindNode(string text)
        {
            foreach (Node n in Model.Nodes)
            {
                if (n.Label.ToString() == text) return n;
            }
            return null;
        }

        /// <summary>
        /// Returns a list of the Child Elements of the parent. returns Null if there aren't any elements
        /// </summary>
        /// <param name="parent">Parent Node</param>
        /// <returns>List of Nodes or null</returns>
        public List<Node> FindChilds(Node parent)
        {
            var childs = new List<Node>();
            foreach (LineConnector c in Model.Connections)
            {
                if (c.HeadNode == parent)
                {
                    childs.Add((Node)c.TailNode);
                }
            }
            if (childs.Count > 0) return childs;
            else return null;
        }

        /// <summary>
        /// returns a list of all Childs from the parent element. This is recursive
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public List<Node> FindChilds(Node parent, bool recursive)
        {
            if (recursive)
            {
                List<Node> res = new List<Node>();

                var t = FindChilds(parent);
                if (t.Count > 0)
                {
                    foreach (Node n in t)
                    {
                        res.AddRange(FindChilds(n, true));
                    }
                    res = t;
                }
                return res;
            }
            else return FindChilds(parent);
        }

        /// <summary>
        /// returns a list of all LineConnectors if there are any
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<LineConnector> FindConnectors(Node node)
        {
            var res = new List<LineConnector>();
            foreach (LineConnector c in Model.Connections)
            {
                if (c.TailNode == node || c.HeadNode == node)
                {
                    res.Add(c);
                }
            }
            return res;
        }

        /// <summary>
        /// Clears the diagram
        /// </summary>
        public void Clear()
        {
            this.Model.Nodes.Clear();
            this.Model.Connections.Clear();
            this.UpdateDiagram();
        }
    }
}