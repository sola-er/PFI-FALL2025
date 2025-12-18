using System;
using UnityEngine;

public class SlendyCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            FindFirstObjectByType<WinlLoseManager>().WinChecker(false);
    }
}
