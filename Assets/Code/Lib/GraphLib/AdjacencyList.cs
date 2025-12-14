using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GraphLib3
{
    public class AdjacencyList : IGraph
    {
        public const int NoEdge = -1;
        private List<(int Node, int Cost)>[] adjacencyList;
        private readonly int nNodes;
        public int NodeCount => nNodes;
        private static Comparer<(int Node, int Cost)> nodeComparer = Comparer<(int Node, int Cost)>.Create((a, b) => a.Node.CompareTo(b.Node));
        public AdjacencyList(int n)
        {
            //peut lancer une exception si n < 0
            adjacencyList = new List<(int Node, int Cost)>[n];
            nNodes = n;
            //(valeur par défaut d'une liste est empty. juste créer une liste pour chaque case du tableau.
            for (int i = 0; i != n; ++i)
                adjacencyList[i] = new List<(int Node, int Cost)>();
        }
        public void AddEdge(int nodeA, int nodeB, int cost)
        {
            int indiceNodeB = indiceWithBinarySearch(nodeA, nodeB);
            if (indiceNodeB<0)
            {
                indiceNodeB = ~indiceNodeB;
                adjacencyList[nodeA].Insert(indiceNodeB, (nodeB, cost));
            }
            else
                adjacencyList[nodeA][indiceNodeB] = (nodeB, cost);
        }
        public int GetEdgeCost(int nodeA, int nodeB)
        {
            int indiceNodeB = indiceWithBinarySearch(nodeA, nodeB);
            return indiceNodeB >= 0 ? adjacencyList[nodeA][indiceNodeB].Cost : NoEdge;
        }
        public IEnumerable<(int, int)> GetOutgoingEdges(int node)
        {
            return adjacencyList[node].ToArray();
        }
        public bool HasEdge(int nodeA, int nodeB)
        {
            return indiceWithBinarySearch(nodeA, nodeB) >= 0;
        }
        public void RemoveEdge(int nodeA, int nodeB)
        {
            int indice = indiceWithBinarySearch(nodeA, nodeB);
            if (indice >= 0) //only remove if there IS an edge so no exceptions
                adjacencyList[nodeA].RemoveAt(indice);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (adjacencyList.Length == 0)
                return sb.ToString();
            for (int i =0; i < adjacencyList.Length; ++i)
            {
                sb.Append($"{i}: ");
                for (int j = 0; j != adjacencyList[i].Count; ++j)
                    sb.Append($"{adjacencyList[i][j].ToString()} ");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private int indiceWithBinarySearch(int nodeA, int nodeB)
        {
            // used actual cost in old version dans AddEdge, but it's not actually used so 0 will do in general
            return adjacencyList[nodeA].BinarySearch((nodeB, 0), nodeComparer);
            
        }
    }
}
