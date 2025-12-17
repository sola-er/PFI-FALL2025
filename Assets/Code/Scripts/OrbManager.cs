using System;
using UnityEngine;
using System.Collections.Generic;
using static System.Random;

public class OrbManager : MonoBehaviour
{
    [SerializeField] private GameObject orbPrefab;

    private HashSet<GameObject> spawnedOrbs;
    private List<GameObject> houses;
    public static int orbAmount = 0;
    public int RemainingOrbs()
        => spawnedOrbs.Count;

    private void Awake()
    {
        GameObject[] houseObjects = GameObject.FindGameObjectsWithTag("House");
        
        houses = new List<GameObject>(houseObjects);
        spawnedOrbs = new HashSet<GameObject>();
    }

    private void Start()
    {
        for (int i = 0; i < houses.Count; ++i)
            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                GameObject orb = Instantiate(orbPrefab, houses[i].transform.position + new Vector3(6, 0, 6), Quaternion.identity);
                spawnedOrbs.Add(orb);
                ++orbAmount;
            }
    }

    public void Collect(GameObject orb)
    {
        if (spawnedOrbs.Contains(orb))
        {
            //Debug.Log(orb.name + " is already spawned");

            //Destroy(orb);
            //Debug.Log("orb destroyed");
            spawnedOrbs.Remove(orb);
            --orbAmount;
        }
    }

    private void OnValidate()
    {
        Debug.Assert(orbPrefab != null);
    }
}
