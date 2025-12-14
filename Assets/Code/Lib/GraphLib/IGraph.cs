using System.Collections.Generic;

namespace GraphLib3
{
    public interface IGraph
    {
        public int NodeCount { get; }
        public void AddEdge(int nodeA, int nodeB, int cost);
        public void RemoveEdge(int nodeA, int nodeB);
        public int GetEdgeCost(int nodeA, int nodeB);
        public bool HasEdge(int nodeA, int nodeB);
        public IEnumerable<(int, int)> GetOutgoingEdges(int node);
    }
}
