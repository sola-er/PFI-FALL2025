using UnityEditor;
using UnityEngine;

public class TriangleWaveMotion : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 1.0f;

    [SerializeField]
    private float frequency = 1.0f;

    [SerializeField]
    private Vector3 direction = Vector3.up;

    [SerializeField]
    private Space space = Space.Self;

    private float phase = 0;
    private float translationPrécédente = 0;

    private void OnValidate()
    {
        direction = direction.normalized;
    }
    private void Update()
    {
        //float translation0=0; resets every time
        phase = phase + frequency * Time.deltaTime;
        float translationActuelle = MathUtils.TriangleWave(amplitude, phase);
        transform.Translate((translationActuelle - translationPrécédente) * direction, space);
        translationPrécédente = translationActuelle;
    }
}

