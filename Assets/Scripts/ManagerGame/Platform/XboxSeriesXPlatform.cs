using UnityEngine;

public class XboxSeriesXPlatform : MonoBehaviour
{
    void Start()
    {
#if UNITY_XBOXONE || UNITY_XBOXONE
        print("Xbox Series X: plataforma detectada. Implementação específica ativada.");
#else
        print("Xbox Series X: rodando em uma plataforma diferente (modo de fallback)." );
#endif
    }

    public string GetPlatformName()
    {
#if UNITY_XBOXONE || UNITY_XBOXONE
        return "Xbox Series X";
#else
        return "Xbox Series X (fallback)";
#endif
    }
}