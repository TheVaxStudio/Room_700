using UnityEngine;

public static class Utils
{
    public static int RandomRange(int Min, int Max)
    {
        return Random.Range(Min, Max);
    }

    public static float Clamp(float Value, float Min, float Max)
    {
        return Mathf.Clamp(Value, Min, Max);
    }
}