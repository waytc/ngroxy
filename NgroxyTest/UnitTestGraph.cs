using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;

namespace NgroxyTest
{
    [TestClass]
    public class UnitTestGraph
    {
        public class Node
        {
            public string Name { get; set; }

            /// <inheritdoc />
            public override string ToString() => Name;
        }


        [TestMethod]
        public void TestMethod1()
        {
            var graph = new BidirectionalGraph<Node, Edge<Node>>();
            var a = new Node {Name = "A"};
            var b = new Node {Name = "B"};
            var c = new Node {Name = "C"};
            var d = new Node {Name = "D"};
            var e = new Node {Name = "E"};
            var f = new Node {Name = "F"};
            var g = new Node {Name = "G"};
            graph.AddVertex(a);
            graph.AddVertex(b);
            graph.AddVertex(c);
            graph.AddVertex(d);
            graph.AddVertex(e);
            graph.AddVertex(f);
            graph.AddVertex(g);
            graph.AddEdge(new Edge<Node>(a, b));
            graph.AddEdge(new Edge<Node>(a, c));
            graph.AddEdge(new Edge<Node>(a, f));
            graph.AddEdge(new Edge<Node>(f, c));
            graph.AddEdge(new Edge<Node>(f, e));
            graph.AddEdge(new Edge<Node>(f, g));
            graph.AddEdge(new Edge<Node>(f, g));
            graph.AddEdge(new Edge<Node>(e, g));
            graph.AddEdge(new Edge<Node>(d, e));
            graph.AddEdge(new Edge<Node>(d, c));
            graph.AddEdge(new Edge<Node>(b, d));

//            var mm = Abc(graph, a, c);
        }

        public Edge<Node> Abc(BidirectionalGraph<Node, Edge<Node>> grpah, Node source, Node target)
        {
            var lst = new List<Edge<Node>>();
            lst.AddRange(grpah.InEdges(source));
            lst.AddRange(grpah.OutEdges(source));
            var dim =
                lst.Select(o => new KeyValuePair<Edge<Node>, int>(o, MinRoutes(grpah, source, target, o))).ToList();
            var min = dim.Min(o => o.Value);
            return dim.FirstOrDefault(o => o.Value == min).Key;
        }

        private static int MinRoutes(IBidirectionalIncidenceGraph<Node, Edge<Node>> grpah, Node source, Node target,IEdge<Node> edge,
            int depth = 0)
        {
            Node other;
            if (edge.Target == source)
            {
                other = edge.Source;
            }
            else if (edge.Source == source)
            {
                other = edge.Target;
            }
            else
            {
                throw new InvalidOperationException();
            }
            if (other == target) return ++depth;
            if (other == source) return int.MaxValue;
            var lst = new List<Edge<Node>>();
            lst.AddRange(grpah.InEdges(other));
            lst.AddRange(grpah.OutEdges(other));
            return lst.Min(o => MinRoutes(grpah, other, target, o, ++depth));
        }

        private int Routes(IBidirectionalIncidenceGraph<Node, Edge<Node>> grpah, Node a, IEdge<Node> be, int depth = 0)
        {
            var other = be.Target == a ? be.Source : be.Target;
            var lst = new List<KeyValuePair<int, Edge<Node>>>();

            foreach (var c in grpah.InEdges(a))
            {
                if (c.Source == other) return ++depth;
                lst.Add(new KeyValuePair<int, Edge<Node>>(Routes(grpah, c.Source, c, depth), c));
            }

            foreach (var c in grpah.OutEdges(a))
            {
                if (c.Target == other) return ++depth;
                lst.Add(new KeyValuePair<int, Edge<Node>>(Routes(grpah, c.Target, c, depth), c));
            }

            return lst.Select(o => o.Key).Min();
        }
    }
}
