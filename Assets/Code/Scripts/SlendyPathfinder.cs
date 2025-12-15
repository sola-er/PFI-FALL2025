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
    }
    private void FollowPath()
    {
        Vector3 target = nodeManager.nodes[currentPath[0]].transform.position;

        Vector3 direction = target - transform.position;

        //bouger vers la target
        transform.Translate(direction.normalized *  speed * Time.deltaTime, Space.World);

        // si on l'atteint, enlever de la liste
        if (Vector3.Distance(transform.position, target) < closenessThreshold)
            currentPath.RemoveAt(0);
    }
    private void OnValidate()
    {
        Debug.Assert(nodeManager != null);
        Debug.Assert(player != null);
    }
}
