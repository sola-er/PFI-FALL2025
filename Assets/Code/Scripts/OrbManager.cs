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
            //if (UnityEngine.Random.Range(0, 100) < 50)
        {
            GameObject orb = Instantiate(orbPrefab, houses[i].transform.position + new Vector3(6, 0, 6), Quaternion.identity);
            
            spawnedOrbs.Add(orb);
        }
    }

    public void Collect(GameObject orb)
    {
        if (spawnedOrbs.Contains(orb))
        {
            Debug.Log(orb.name + " is already spawned");
            Destroy(orb);
            Debug.Log("orb destroyed");
            //spawnedOrbs.Remove(orb);
        }
    }

    private void OnValidate()
    {
        Debug.Assert(orbPrefab != null);
    }
    
    public int RemainingOrbs()
        => spawnedOrbs.Count;
}
