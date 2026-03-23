using UnityEngine;

public class NintendoSwitchPlatform : MonoBehaviour
{
    void Start()
    {
#if UNITY_SWITCH
        print("Nintendo Switch: plataforma detectada. Implementação específica ativada.");
#else
        print("Nintendo Switch: rodando em uma plataforma diferente (modo de fallback)." );
#endif
    }

    public string GetPlatformName()
    {
#if UNITY_SWITCH
        return "Nintendo Switch";
#else
        return "Nintendo Switch (fallback)";
#endif
    }
}