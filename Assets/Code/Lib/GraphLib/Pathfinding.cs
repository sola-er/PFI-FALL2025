using GraphLib3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLib3
{
    public static class Pathfinding
    {
        public static IEnumerable<int> TraverseBFS(IGraph graph, int start)
        {
            //Structures de données
            var frontier = new Queue<int>(); //capacité initiale ? à réfléchir...
            var reached = new HashSet<int>();

            //Point de départ (setup)
            frontier.Enqueue(start);
            reached.Add(start);

            //Algorithme BFS
            while (frontier.Count > 0)
            {
                int currentNode = frontier.Dequeue();
                var neighbours = graph.GetOutgoingEdges(currentNode).Select(x => x.Item1);

                foreach (var neighbor in neighbours)
                {
                    //Contains avec un HashSet -> rapide !
                    //Contains avec une liste non-ordonnée -> linéaire...
                    if (!reached.Contains(neighbor))
                    {
                        frontier.Enqueue(neighbor);
                        reached.Add(neighbor);
                    }
                }
            }
            return reached.ToArray();
        }

        public static bool PathExistsBFS(IGraph graph, int start, int end)
        {
            var frontier = new Queue<int>();
            var reached = new HashSet<int>();

            frontier.Enqueue(start);
            reached.Add(start);

            while (frontier.Count > 0)
            {
                int currentNode = frontier.Dequeue();
                var neighbours = graph.GetOutgoingEdges(currentNode).Select(x => x.Item1);

                foreach (var neighbor in neighbours)
                {
                    if (neighbor == end)
                    {
                        return true;
                    }
                    if (!reached.Contains(neighbor))
                    {
                        frontier.Enqueue(neighbor);
                        reached.Add(neighbor);
                    }
                }
            }
            return false;
        }
        ///// DFS //////
        public static IEnumerable<int> TraverseDFS(IGraph graph, int start)
        {
            var reached = new HashSet<int>();
            var frontier = new Stack<int>();
            
            frontier.Push(start);
            reached.Add(start);
            while (frontier.Count > 0)
            {
                int currentNode = frontier.Pop();
                var neighbours = graph.GetOutgoingEdges(currentNode).Select(x => x.Item1);

                foreach (var neighbor in neighbours)
                {
                    if (!reached.Contains(neighbor))
                    {
                        frontier.Push(neighbor);
                        reached.Add(neighbor);
                    }
                }
            }
            return reached.ToArray();
        }
        public static bool PathExistsDFS(IGraph graph, int start, int end)
        {
            var reached = new HashSet<int>();
            var frontier = new Stack<int>();
            frontier.Push(start);
            reached.Add(start);
            while (frontier.Count > 0)
            {
                int currentNode = frontier.Pop();
                var neighbours = graph.GetOutgoingEdges(currentNode).Select(x => x.Item1);
                foreach (var neighbor in neighbours)
                {
                    if (neighbor == end)
                    {
                        return true;
                    }
                    if (!reached.Contains(neighbor))
                    {
                        frontier.Push(neighbor);
                        reached.Add(neighbor);
                    }
                }
            }
            return false;
        }
        public static IEnumerable<int> GetPathBFS(IGraph graph, int start, int end)
        {
            var reached = new HashSet<int>();
            var frontier = new Queue<int>();
            Dictionary<int, int> originOfNode = new Dictionary<int, int>();

            frontier.Enqueue(start);
            reached.Add(start);
            originOfNode[start] = -1; // début n'a pas d'origine

            while (frontier.Count > 0)
            {
                int currentNode = frontier.Dequeue();
                // si la fin est trouvé, reconstruire le chemin d'abord inversé avec le dictionnaire, then reverse et retourner
                if (currentNode == end)
                {
                    List<int> path = new List<int>();
                    int node = end;
                    while (node != -1)
                    {
                        path.Add(node);
                        node = originOfNode[node];
                    }
                    path.Reverse();
                    return path;
                }

                var neighbours = graph.GetOutgoingEdges(currentNode).Select(x => x.Item1);
                foreach (var neighbor in neighbours)
                {
                    if (!reached.Contains(neighbor))
                    {
                        frontier.Enqueue(neighbor);
                        reached.Add(neighbor);
                        originOfNode[neighbor] = currentNode;
                    }
                }
            }
            return Array.Empty<int>(); // si on sort du while, pas de chemin vers end trouvé!
        }

        public static IEnumerable<int> GetPathDFS(IGraph graph, int start, int end)
        {
            var reached = new HashSet<int>();
            var frontier = new Stack<int>();
            Dictionary<int, int> originOfNode = new Dictionary<int, int>();

            frontier.Push(start);
            reached.Add(start);
            originOfNode[start] = -1;

            while (frontier.Count > 0)
            {
                int currentNode = frontier.Pop();
                if (currentNode == end)
                {
                    List<int> path = new List<int>();
                    int node = end;
                    while (node != -1)
                    {
                        path.Add(node);
                        node = originOfNode[node];
                    }
                    path.Reverse();
                    return path;
                }

                var neighbours = graph.GetOutgoingEdges(currentNode).Select(x => x.Item1);
                foreach (var neighbor in neighbours)
                {
                    if (!reached.Contains(neighbor))
                    {
                        frontier.Push(neighbor);
                        reached.Add(neighbor);
                        originOfNode[neighbor] = currentNode;
                    }
                }
            }
            return Array.Empty<int>();
        }
        public static IEnumerable<int> GetPathDijkstra(IGraph graph, int start, int end)
        {
            //frontier pour explorer, origin pour retracer le chemin, cost pour garder le cout minimal
            var frontier = new PriorityQueue<int>();
            Dictionary<int, int> originOfNode = new Dictionary<int, int>();
            Dictionary<int, int> costSoFar = new Dictionary<int, int>();

            //initialisation
            frontier.Enqueue(start, 0);
            originOfNode[start] = -1;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                int currentNode = frontier.Dequeue();

                if (currentNode == end)
                {
                    List<int> path = new List<int>();
                    int node = end;
                    while (node != -1)
                    {
                        path.Add(node);
                        node = originOfNode[node];
                    }
                    path.Reverse();
                    return path;
                }
                var neighbours = graph.GetOutgoingEdges(currentNode).Select(x => x.Item1);
                foreach (var neighbor in neighbours)
                {
                    int newCost = costSoFar[currentNode] + graph.GetEdgeCost(currentNode, neighbor);
                    if(!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                    {
                        costSoFar[neighbor] = newCost;

                        frontier.Enqueue(neighbor, newCost);
                        originOfNode[neighbor] = currentNode;
                    }
                }
            }
            return Array.Empty<int>();
        }
    }
}
