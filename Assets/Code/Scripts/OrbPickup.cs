using UnityEngine;

public class OrbPickup : MonoBehaviour
{
    private OrbManager orbManager;
    [SerializeField] private GameObject slendyGO;
    [SerializeField] private SlendyPathfinder slendy;
    [SerializeField] private float orbForce = 10f;
    private void Awake()
    {
        orbManager = FindFirstObjectByType<OrbManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            slendy.ApplyOrbForce(orbForce);
            Destroy(gameObject);
            orbManager.Collect(gameObject);
        }
    }
}
