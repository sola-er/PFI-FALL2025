using System;
using UnityEngine;
using System.Collections.Generic;
using static System.Random;

public class OrbManager : MonoBehaviour
{
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private int spawnRatePercentage = 50;
    [SerializeField] private float orbPushbackForce = 10f;

    private HashSet<GameObject> spawnedOrbs;
    private List<GameObject> houses;
    public int orbAmount = 0;
    private SlendyActions slendyActions;
    private void Awake()
    {
        // find all houses to later spawn orbs in some
        GameObject[] houseObjects = GameObject.FindGameObjectsWithTag("House");
        houses = new List<GameObject>(houseObjects);
        spawnedOrbs = new HashSet<GameObject>(); // hashset for more efficient removal later

        slendyActions = FindFirstObjectByType<SlendyActions>();
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
        if (spawnedOrbs.Remove(orb)) // only process if orb was in the set, and so successfully removed from it
            //note on the hashset: not strictly necessary since only orbAmount matters for the win,
            //but its kind of a failsafe in case of double-collection, orbs collecting lag...
            //kind of defensive coding but we've had a lot of bugs
        {
            ProcessCollectedOrb(orb);
            CheckForWin();
            slendyActions.ApplyOrbForce(orbPushbackForce); // push Slendy back when an orb is collected
        }
    }
    private void CheckForWin()
    {
        if (orbAmount == 0)
            FindFirstObjectByType<WinlLoseManager>().CheckWinCondition(true);
        // the findfirst could be in awake to optimize,
        // but it'll only be called once anyway (orbAmount hits 0 once)
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
