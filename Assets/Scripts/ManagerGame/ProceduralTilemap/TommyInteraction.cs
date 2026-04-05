using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommyInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float InteractionRange = 3f; // Distância máxima para interação
    public LayerMask InteractableLayer; // Layer dos objetos interagíveis
    public KeyCode InteractKey = KeyCode.E; // Tecla para interagir

    [Header("UI")]
    public GameObject InteractionPrompt; // UI para mostrar prompt de interação

    private GameObject currentInteractable;

    void Update()
    {
        // Raycast para detectar objetos interagíveis
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionRange, InteractableLayer))
        {
            if (hit.collider.gameObject != currentInteractable)
            {
                // Novo objeto interagível
                currentInteractable = hit.collider.gameObject;
                ShowInteractionPrompt(true);
            }
        }
        else
        {
            // Nenhum objeto interagível
            currentInteractable = null;
            ShowInteractionPrompt(false);
        }

        // Interagir se tecla pressionada
        if (Input.GetKeyDown(InteractKey) && currentInteractable != null)
        {
            Interact(currentInteractable);
        }
    }

    void ShowInteractionPrompt(bool show)
    {
        if (InteractionPrompt != null)
        {
            InteractionPrompt.SetActive(show);
        }
    }

    void Interact(GameObject obj)
    {
        // Lógica de interação baseada no tipo de objeto
        if (obj.CompareTag("Door"))
        {
            // Abrir porta
            Door door = obj.GetComponent<Door>();
            if (door != null)
            {
                door.Open();
            }
        }
        else if (obj.CompareTag("Key"))
        {
            // Coletar chave
            Key key = obj.GetComponent<Key>();
            if (key != null)
            {
                key.Collect();
            }
        }
        else if (obj.CompareTag("Bed"))
        {
            // Interagir com cama (ex: dormir)
            Bed bed = obj.GetComponent<Bed>();
            if (bed != null)
            {
                bed.Interact();
            }
        }

        Debug.Log("Interagindo com: " + obj.name);
    }

    // Método para interação programática
    public void ForceInteract(GameObject obj)
    {
        Interact(obj);
    }
}