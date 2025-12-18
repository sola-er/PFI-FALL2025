using UnityEngine;

public static class MathUtils
{
    public static float SineWave(float amplitude, float phase)
    {
        return amplitude * Mathf.Sin(2f * Mathf.PI * phase);
    }
    public static float TriangleWave(float amplitude, float phase)
    {
        return amplitude * (1f - Mathf.Abs((phase % 1f) * 2f - 1f));
    }
}