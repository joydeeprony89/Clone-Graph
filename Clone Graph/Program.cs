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
           if(node == null) return node;
          // We need to perform DFS here
          // Will take Queue for DFS, will push the node initially to q to start DFS, as question says "The given node will always be the first node with val = 1."
          // in DFS using Q, we add a condition while(q is not empty){} in inside we push each node during DFS traversal for a particular node.
          // Will be using Dictioanry<int, Node> as the visited DS to check if any node is already processed or not and also for the output for this problem. 
          // in visited will be pushing a new node(val) always, which will be used later to add the neighbours.
          // initially will be pushing node to Q and also to visited(here will be creating a new node with the node.val, visited.Add(node.val, new Node(node.val))
          Queue<Node> q = new Queue<Node>();
          var clone = new Node(node.val);
          Dictionary<int, Node> visited = new Dictionary<int, Node>();
          q.Enqueue(node);
          visited.Add(node.val, clone);// later will take out the clone node from visited and add its neighbours
          while(q.Count > 0) {
            var temp = q.Dequeue();
            var neighbors = temp.neighbors;
            foreach(var n in neighbors) {
              if(!visited.ContainsKey(n.val)) {
                q.Enqueue(n);
                visited.Add(n.val, new Node(n.val)); // here we are cloning each neighbour node and adding to visited, later will be taking them from visited and set their neighbours
              }
              var clonedNode = visited[temp.val];
              clonedNode.neighbors.Add(visited[n.val]);
            }
          }

          return clone;
        }
    }
}
