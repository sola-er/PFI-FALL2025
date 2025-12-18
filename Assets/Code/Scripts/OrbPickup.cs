using UnityEngine;

public class OrbPickup : MonoBehaviour
{
    private OrbManager orbManager;
    private void Awake()
    {
        orbManager = FindFirstObjectByType<OrbManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            orbManager.Collect(gameObject);
        }
    }
}
