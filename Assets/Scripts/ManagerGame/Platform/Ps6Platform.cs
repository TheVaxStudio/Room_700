using UnityEngine;

public class Ps6Platform : MonoBehaviour
{
    public string GetPlatformName()
    {
#if UNITY_PS6
        return "PlayStation 6";
#else
        return "PlayStation 6 (fallback)";
#endif
    }
}