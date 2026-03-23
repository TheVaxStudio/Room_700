using UnityEngine;
using TMPro;

public class KeyPickup : MonoBehaviour
{
    public HudCutscene HudCutscene; // arraste no inspetor

    AudioSource KeyPickupSound;
    
    void Awake()
    {
        KeyPickupSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D Key)
    {
        if (Key.gameObject.tag == "KeyRoom08")
        {
            KeyPickupSound.Play();
            
            Key.gameObject.SetActive(false);

            if (HudCutscene != null)
            {
                HudCutscene.OnKeyCollected();
            }
        }
    }
}