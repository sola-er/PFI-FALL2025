using System;
using UnityEngine;
using System.Collections.Generic;
using static System.Random;

public class OrbManager : MonoBehaviour
{
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private int spawnRatePercentage = 50;

    private HashSet<GameObject> spawnedOrbs;
    private List<GameObject> houses;
    public int orbAmount = 0;

    private void Awake()
    {
        GameObject[] houseObjects = GameObject.FindGameObjectsWithTag("House");
        
        houses = new List<GameObject>(houseObjects);
        spawnedOrbs = new HashSet<GameObject>();
    }

    private void Start()
    {
        InitializeOrbs();
    }

    private void InitializeOrbs()
    {
        for (int i = 0; i < houses.Count && orbAmount < 5; ++i)
            if (UnityEngine.Random.Range(0, 100) < spawnRatePercentage)
            {
                GameObject orb = Instantiate(orbPrefab, houses[i].transform.position, Quaternion.identity);
                spawnedOrbs.Add(orb);
                ++orbAmount;
            }
    }

    public void Collect(GameObject orb)
    {
        if (spawnedOrbs.Remove(orb))
        {
            ProcessCollectedOrb(orb);
            CheckForWin();
        }
    }

    private void CheckForWin()
    {
        if (orbAmount == 0)
            FindFirstObjectByType<WinlLoseManager>().WinChecker(true);
    }

    private void ProcessCollectedOrb(GameObject orb)
    {
        --orbAmount;
        Destroy(orb);
    }

    private void OnValidate()
    {
        Debug.Assert(orbPrefab != null);
    }
}
