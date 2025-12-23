using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlendyActions : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // compute the direction towards the player and apply the opposite of it for pushback
    public void ApplyOrbForce(float forceMagnitude)
    {
        Vector3 direction = player.position - transform.position;
        Vector3 force = -direction.normalized * forceMagnitude;
        rb.AddForce(force, ForceMode.Impulse);
    }
    // on collision with player, trigger loss
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            FindFirstObjectByType<WinlLoseManager>().CheckWinCondition(false);
        // like in OrbManager, could get the WinlLoseManager reference in Awake to optimize,
        // but this will only be called once per game anyway
        // also: FindFirstObjectByType is kinda expensive, but seemed okay at this scale
    }
}
