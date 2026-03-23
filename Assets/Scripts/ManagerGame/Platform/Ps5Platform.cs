using UnityEngine;

public class Ps5Platform : MonoBehaviour
{
    void Start()
    {
#if UNITY_PS5
        print("PS5: plataforma detectada. Implementação específica ativada.");
#else
        print("PS5: rodando em uma plataforma diferente (modo de fallback)." );
#endif
    }

    public string GetPlatformName()
    {
#if UNITY_PS5
        return "PlayStation 5";
#else
        return "PlayStation 5 (fallback)";
#endif
    }
}