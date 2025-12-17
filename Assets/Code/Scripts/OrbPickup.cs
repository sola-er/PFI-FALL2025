using UnityEngine;

public class OrbPickup : MonoBehaviour
{
    [SerializeField] private OrbManager orbManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            orbManager.Collect(gameObject);
        }
    }
    private void OnValidate()
    {
        Debug.Assert(orbManager != null);
    }
}
