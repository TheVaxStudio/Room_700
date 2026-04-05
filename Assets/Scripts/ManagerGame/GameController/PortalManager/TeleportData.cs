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