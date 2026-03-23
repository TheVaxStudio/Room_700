using UnityEngine;

public class KeyPickupRoom20 : MonoBehaviour
{
    public HudCutscene HudCutscene;

    void OnCollisionEnter2D(Collision2D Key)
    {
        if (Key.gameObject.tag == "KeyRoom20")
        {
            Key.gameObject.SetActive(false);

            if (HudCutscene != null)
            {
                HudCutscene.TriggerCutscene();
            }
        }
    }
}