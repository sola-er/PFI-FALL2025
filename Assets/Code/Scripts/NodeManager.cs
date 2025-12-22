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
        InitializeGraph();
    }
    private void InitializeGraph()
    {
        //find all nodes in scene, put them in a list and create graph the size of that list
        GameObject[] nodeObjects = GameObject.FindGameObjectsWithTag("Node");
        nodes = new List<GameObject>(nodeObjects);
        graph = new AdjacencyList(nodes.Count);

        // for each node, check distance to every other node. if close enough, create edge between them
        // O(n^2), but at this scale it works
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

    // for future use by pathfinding: send current position, get closest node to it.
    public int FindClosestNode(Vector3 pos)
    {
        int closestNode = 0;
        float closestDistance = float.MaxValue; // just so its rlly far away and we're sure to find something closer

        for (int i = 0; i != nodes.Count; ++i)
        {
            float currentDistance = Vector3.Distance(pos, nodes[i].transform.position);
            if (currentDistance < closestDistance)
            {
                closestNode = i;
                closestDistance = currentDistance;
            }
        }
        return closestNode;
    }
}
