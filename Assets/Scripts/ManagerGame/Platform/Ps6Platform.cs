using UnityEngine;

public class Ps6Platform : MonoBehaviour
{
    void Start()
    {
#if UNITY_PS6
        print("PS6: plataforma detectada. Implementação específica ativada.");
#else
        print("PS6: plataforma ainda não suportada diretamente pelo Unity (modo de fallback)." );
#endif
    }

    public string GetPlatformName()
    {
#if UNITY_PS6
        return "PlayStation 6";
#else
        return "PlayStation 6 (fallback)";
#endif
    }
}