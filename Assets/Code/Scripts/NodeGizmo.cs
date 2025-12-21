using UnityEngine;

public class NodeGizmo : MonoBehaviour
{
    [SerializeField] private float radius = 0.25f;
    [SerializeField] private Color color = Color.yellow;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}