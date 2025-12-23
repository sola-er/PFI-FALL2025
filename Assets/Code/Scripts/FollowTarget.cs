using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void LateUpdate() // LateUpdate to make sure it follows after target has moved
    {
        transform.position = new Vector3(target.transform.position.x, 
                                         transform.position.y, // stay at the same elevation
                                         target.transform.position.z);
    }
}
