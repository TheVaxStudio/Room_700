using UnityEngine;

namespace HisaGames.Props
{
    [System.Serializable]
    public class EcProps : MonoBehaviour
    {
        [Tooltip("Time speed for the prop to fade in and become visible.")]
        float fadeSpeed;

        public void SetVisibility(float fadeSpeed)
        {
            this.fadeSpeed = fadeSpeed;
        }

        public void PropUpdate()
        {
            var step = fadeSpeed * Time.deltaTime;

            if (step != 0)
            {
                if (transform.localScale != Vector3.one)
                {
                    transform.localScale = Vector3.MoveTowards(transform.localScale,
                    Vector3.one, step);
                }
            }

            else
            {
                transform.localScale = Vector3.one;
            }
        }
    }
}