using UnityEngine;

public class NintendoSwitchPlatform : MonoBehaviour
{
    public string GetPlatformName()
    {
#if UNITY_SWITCH
        return "Nintendo Switch";
#else
        return "Nintendo Switch (fallback)";
#endif
    }
}