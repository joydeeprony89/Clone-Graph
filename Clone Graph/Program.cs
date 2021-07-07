using System;
using System.Collections.Generic;

namespace Clone_Graph
{
    class Program
    {
        public class Node
        {
            public int val;
            public List<Node> neighbors;

            public Node()
            {
                val = 0;
                neighbors = new List<Node>();
            }

            public Node(int _val)
            {
                val = _val;
                neighbors = new List<Node>();
            }

            public Node(int _val, List<Node> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }
        }

        static void Main(string[] args)
        {

            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);

            node1.neighbors = new List<Node>() { node2, node4 };
            node2.neighbors = new List<Node>() { node1, node3 };
            node3.neighbors = new List<Node>() { node2, node4 };
            node4.neighbors = new List<Node>() { node1, node3 };
            Node cloned = CloneGraph_Iterative(node1);
            Console.ReadKey();
        }

        static Dictionary<Node, Node> kv = new Dictionary<Node, Node>();
        static Node CloneGraph(Node node)
        {
            if (node == null) return node;
            if (kv.ContainsKey(node)) return kv[node];
            Node clone = new Node(node.val);
            kv.Add(node, clone);
            foreach (var item in node.neighbors)
            {
                clone.neighbors.Add(CloneGraph(item));
            }
            return clone;
        }

        static Node CloneGraph_Iterative(Node node)
        {
            if (node == null) return null;
            Dictionary<Node, Node> keyValuePairs = new Dictionary<Node, Node>();
            Queue<Node> queue = new Queue<Node>();
            Node clone = new Node(node.val);
            queue.Enqueue(node);
            keyValuePairs.Add(node, clone);
            while(queue.Count > 0)
            {
                Node popped = queue.Dequeue();
                foreach(var n in popped.neighbors)
                {
                    if (!keyValuePairs.ContainsKey(n))
                    {
                        keyValuePairs.Add(n, new Node(n.val));
                        queue.Enqueue(n);
                    }
                    keyValuePairs[popped].neighbors.Add(keyValuePairs[n]);
                }
            }

            return clone;
        }
    }
}
