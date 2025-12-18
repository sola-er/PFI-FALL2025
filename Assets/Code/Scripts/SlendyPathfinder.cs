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
    [SerializeField] private float closenessThresholdNode = 0.5f;
    [SerializeField] private float closenessThresholdPlayer = 100f;

    private float elapsedTime = 0f;
    private List<int> currentPath;
    private int nextNodeIndex = 0;

    private void Update()
    {
        // pathfinding � chaque seconde (1f)
        if (elapsedTime >= tempsAvantTraitements)
        {
            Traitements();
            elapsedTime -= tempsAvantTraitements;
        }
        elapsedTime += Time.deltaTime;

        FollowPath();
        
        if (Vector3.Distance(transform.position, player.transform.position) <= closenessThresholdPlayer)
            FollowPlayer();
    }
    private void Traitements()
    {
        int start = nodeManager.FindClosestNode(transform.position);
        int end = nodeManager.FindClosestNode(player.transform.position);

        currentPath = new List<int>(Pathfinding.GetPathDijkstra(nodeManager.graph, start, end));
        nextNodeIndex = 0;
        Debug.Log($"New path: from node {start} to {end}. Path length is {currentPath.Count}");

        //if (Vector3.Distance(transform.position, player.transform.position) <= closenessThresholdPlayer)
            //FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 target = new Vector3(player.position.x, 
            transform.position.y, // reste sur axe des y so he doesnt tweak tf out
            player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    private void FollowPath()
    {
        if (currentPath == null || currentPath.Count == 0)
        {
            Debug.Log("No path");
            return;
        }

        //int nextNodeIndex = currentPath[0];
        Vector3 target = nodeManager.nodes[nextNodeIndex].transform.position;

        // ignore Y so Slendy stays on ground
        Vector3 currentPosXZ = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetXZ = new Vector3(target.x, transform.position.y, target.z); // use slendy's current Y

        // move toward target (possibly change t.position assign?? t.translate? addforce? idk
        transform.position = Vector3.MoveTowards(transform.position, targetXZ, speed * Time.deltaTime);

        // if close enough to node, remove it
        if (Vector3.Distance(transform.position, targetXZ) < closenessThresholdNode)
        {
            //currentPath.RemoveAt(0);
            nextNodeIndex++;
        }
    }
    private void OnValidate()
    {
        Debug.Assert(nodeManager != null, "NodeManager reference is missing");
        Debug.Assert(player != null, "Player reference missing");
    }
} 
