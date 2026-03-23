using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PortalPair
{
    public int id;

    public GameObject portalA;

    public GameObject portalB;
}

public class TeleportData : MonoBehaviour
{
    [Header("Pares de Portais Bidirecionais")]
    [SerializeField]
    List<PortalPair> portalPairs = new List<PortalPair>();

    Dictionary<GameObject, GameObject> teleportDictionary
    {
        get;

        set;
    }

    void Awake()
    {
        teleportDictionary = new Dictionary<GameObject, GameObject>();

        foreach (var pair in portalPairs)
        {
            if (pair.portalA != null && pair.portalB != null)
            {
                if (!teleportDictionary.ContainsKey(pair.portalA))
                {
                    teleportDictionary.Add(pair.portalA, pair.portalB);
                }

                if (!teleportDictionary.ContainsKey(pair.portalB))
                {
                    teleportDictionary.Add(pair.portalB, pair.portalA);
                }
            }
        }
    }
    
    public GameObject GetDestination(GameObject portal)
    {
        if (teleportDictionary.ContainsKey(portal))
        {
            return teleportDictionary[portal];
        }

        else
        {
            Debug.LogWarning($"Nenhum destino encontrado para o portal {portal.name}");

            return null;
        }
    }
}