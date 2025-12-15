using UnityEngine;
using System.Collections.Generic;
using GraphLib3;
using System;

public class SlendyPathfinder : MonoBehaviour
{
    [SerializeField] private float tempsAvantTraitements = 1f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private NodeManager nodeManager;
    [SerializeField] private Transform player;
    [SerializeField] private float closenessThreshold = 0.5f;

    private float elapsedTime = 0f;
    private List<int> currentPath;

    private void Update()
    {
        // Run pathfinding every tempsAvantTraitements seconds
        if (elapsedTime >= tempsAvantTraitements)
        {
            Traitements();
            elapsedTime -= tempsAvantTraitements;
        }
        elapsedTime += Time.deltaTime;

        FollowPath();
    }

    private void Traitements()
    {
        int start = nodeManager.FindClosestNode(transform.position);
        int end = nodeManager.FindClosestNode(player.transform.position);

        currentPath = new List<int>(Pathfinding.GetPathDijkstra(nodeManager.graph, start, end));
        Debug.Log($"New path calculated from node {start} to {end}. Path length: {currentPath.Count}");
    }

    private void FollowPath()
    {
        if (currentPath == null || currentPath.Count == 0)
        {
            Debug.Log("No path to follow.");
            return;
        }

        int nextNodeIndex = currentPath[0];
        Vector3 target = nodeManager.nodes[nextNodeIndex].transform.position;

        // Ignore Y axis so Slendy stays on ground
        Vector3 currentPosXZ = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetXZ = new Vector3(target.x, 0f, target.z);

        Debug.Log($"Slendy Pos: {transform.position} | Next Node Index: {nextNodeIndex} | Target Pos: {target} | Distance (XZ): {Vector3.Distance(currentPosXZ, targetXZ)}");

        // Move toward target (XZ only), keep current Y
        Vector3 newPos = Vector3.MoveTowards(
            transform.position,
            new Vector3(target.x, transform.position.y, target.z),
            speed * Time.deltaTime
        );
        transform.position = newPos;

        // If close enough in XZ plane, remove node
        if (Vector3.Distance(currentPosXZ, targetXZ) < closenessThreshold)
        {
            Debug.Log($"Reached node {nextNodeIndex}, removing from path.");
            currentPath.RemoveAt(0);

            if (currentPath.Count > 0)
            {
                Debug.Log($"Next node will be {currentPath[0]} at {nodeManager.nodes[currentPath[0]].transform.position}");
            }
            else
            {
                Debug.Log("Path finished.");
            }
        }
    }

    private void OnValidate()
    {
        Debug.Assert(nodeManager != null, "NodeManager reference is missing!");
        Debug.Assert(player != null, "Player reference is missing!");
        Debug.Log("SlendyPathfinder validated successfully.");
    }
}
