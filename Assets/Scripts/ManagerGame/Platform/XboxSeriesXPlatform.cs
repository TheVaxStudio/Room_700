using UnityEngine;

public class XboxSeriesXPlatform : MonoBehaviour
{
    public string GetPlatformName()
    {
#if UNITY_XBOXONE || UNITY_XBOXONE
        return "Xbox Series X";
#else
        return "Xbox Series X (fallback)";
#endif
    }
}