using UnityEngine;

public class Ps5Platform : MonoBehaviour
{
    public string GetPlatformName()
    {
#if UNITY_PS5
        return "PlayStation 5";
#else
        return "PlayStation 5 (fallback)";
#endif
    }
}