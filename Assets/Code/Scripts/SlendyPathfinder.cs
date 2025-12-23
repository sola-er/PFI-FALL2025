using UnityEngine;
using System.Collections.Generic;
using GraphLib3;
using System;

[RequireComponent(typeof(Rigidbody))]
public class SlendyPathfinder : MonoBehaviour
{
    [SerializeField] private float tempsAvantTraitements = 1f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private NodeManager nodeManager;
    [SerializeField] private Transform player;
    [SerializeField] private float closenessThresholdNode = 0.5f; // how close to a node before going to the next
    [SerializeField] private float closenessThresholdPlayer = 60f; // how close to player before abandoning pathfinding

    private Rigidbody rb;
    private float elapsedTime = 0f;
    private List<int> currentPath;
    private int nextNodeIndex = 0;
    int lastStart = -1; // invalid values so first pathfinding works
    int lastEnd = -1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnValidate()
    {
        Debug.Assert(nodeManager != null);
        Debug.Assert(player != null);
    }

    private void Update()
    {
        CheckForTreatment();
    }
    // treatment in update and actual physics-based movement in fixedupdate
    private void FixedUpdate()
    {
        if (IsCloseEnoughToPlayer())
            FollowPlayer();
        else
            FollowPath();
    }
    private bool IsCloseEnoughToPlayer() =>
        Vector3.Distance(transform.position, player.transform.position) <= closenessThresholdPlayer;
    private void CheckForTreatment()
    {
        // only recalculate path every tempsAvantTraitements seconds
        if (elapsedTime >= tempsAvantTraitements)
        {
            FindPath();
            elapsedTime -= tempsAvantTraitements;
        }
        elapsedTime += Time.deltaTime;
    }

    private void FindPath()
    {
        // compute current start and end
        int start = nodeManager.FindClosestNode(transform.position);
        int end = nodeManager.FindClosestNode(player.transform.position);

        // if theyre the same as before, no need to find a new path
        if (lastStart == start && lastEnd == end)
            return;

        lastStart = start;
        lastEnd = end;

        currentPath = new List<int>(Pathfinding.GetPathDijkstra(nodeManager.graph, start, end));
        nextNodeIndex = 0;

    }

    private void FollowPlayer()
    {
        Vector3 target = new Vector3(player.position.x, 
            transform.position.y, // stay at the same elevation
            player.position.z);
        rb.MovePosition(Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime));
    }
    private void FollowPath()
    {
        if (currentPath == null || currentPath.Count == 0)
        {
            Debug.Log("No path");
            return;
        }
        
        int nodeId = currentPath[nextNodeIndex];
        Vector3 target = nodeManager.nodes[nodeId].transform.position;

        // ignore target's pos.y bc terrain is uneven
        Vector3 currentPosXZ = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 targetXZ = new Vector3(target.x, transform.position.y, target.z); // pos.y is slendy's

        // move toward target 
        rb.MovePosition(Vector3.MoveTowards(transform.position, targetXZ, speed * Time.fixedDeltaTime));

        // if close enough to node, head towards the next
        if (Vector3.Distance(currentPosXZ, targetXZ) < closenessThresholdNode)
        {
            nextNodeIndex++;
            if (nextNodeIndex >= currentPath.Count)
            {
                currentPath = null;
                return;
            }
        }
    }
} 
