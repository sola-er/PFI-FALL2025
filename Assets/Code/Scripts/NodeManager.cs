using UnityEngine;
using GraphLib3;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private float maxDistanceForConnection = 100f;
    public AdjacencyList graph;
    public List<GameObject> nodes;

    private void Awake()
    {
        InitialiserGraph();
    }
    private void InitialiserGraph()
    {
        GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag("Node");
        nodes = new List<GameObject>(nodeObjects);
        graph = new AdjacencyList(nodes.Count);

        for (int i = 0; i != nodes.Count; ++i)
        {
            for (int j = i + 1; j != nodes.Count; ++j)
            {
                float distance = Vector3.Distance(nodes[i].transform.position, nodes[j].transform.position);
                if (distance < maxDistanceForConnection)
                {
                    graph.AddEdge(i, j, (int)distance);
                    graph.AddEdge(j, i, (int)distance);
                }
            }
        }
    }

    // for future use by slendy/player: send current position, get closest node to it.
    public int FindClosestNode(Vector3 pos)
    {
        int closestNode = 0;
        float distance = float.MaxValue; // just so its rlly far away yk

        for (int i = 0; i != nodes.Count; ++i)
        {
            float currentDistance = Vector3.Distance(pos, nodes[i].transform.position);
            if (currentDistance < distance)
            {
                closestNode = i;
                distance = currentDistance;
            }
        }
        return closestNode;
    }
    //testing if all nodes are connected
    //private void Testing()
    //{
    //    // Run BFS from node 0
    //    var reached = Pathfinding.TraverseBFS(graph, 0);

    //    int reachedCount = 0;
    //    foreach (int node in reached)
    //        reachedCount++;

    //    if (reachedCount == nodes.Count)
    //        Debug.Log(":) All nodes are connected!");
    //    else
    //        Debug.LogWarning($" :( Only {reachedCount}/{nodes.Count} nodes reachable. Some nodes are isolated!");
    //}
}
