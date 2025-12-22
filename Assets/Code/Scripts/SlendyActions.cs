using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlendyActions : MonoBehaviour
{
    [SerializeField] private Transform player;
    Rigidbody rb;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            FindFirstObjectByType<WinlLoseManager>().WinChecker(false);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void ApplyOrbForce(float forceMagnitude)
    {
        Vector3 direction = player.position - transform.position;
        Vector3 force = -direction.normalized * forceMagnitude;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
