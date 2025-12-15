using System;
using UnityEngine;
using System.Collections.Generic;
using static System.Random;

public class OrbManager : MonoBehaviour
{
    [SerializeField] private GameObject orbPrefab;

    private List<GameObject> spawnedOrbs = new List<GameObject>();
    private List<GameObject> houses;

    private void Awake()
    {
        GameObject[] houseObjects = GameObject.FindGameObjectsWithTag("House");
        houses = new List<GameObject>(houseObjects);
        spawnedOrbs = new List<GameObject>();
    }

    private void Start()
    {
        for (int i = 0; i < houses.Count; ++i)
            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                GameObject orb = Instantiate(orbPrefab, houses[i].transform);
                spawnedOrbs.Add(orb);
            }
    }

    private void Collect(GameObject orb)
    {
        if (spawnedOrbs.Contains(orb))
        {
            Destroy(orb);
            spawnedOrbs.Remove(orb);
        }

        if (RemainingOrbs() == 0)
            Debug.Log("gg senpai >.<");
    }
    
    public int RemainingOrbs()
        => spawnedOrbs.Count;
}
