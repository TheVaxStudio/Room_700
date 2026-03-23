using UnityEngine;

public class FootStepSystem : MonoBehaviour
{
    [Header("Áudio")]
    public AudioSource Source;

    public AudioClip[] FootStepClips;

    [Header("Randomização")]
    [Range(0.0f, 0.3f)]
    public float PitchVariation = 0.15f;

    [Range(0.0f, 0.3f)] 
    public float VolumeVariation = 0.15f;

    public void PlayFootstep()
    {
        if (FootStepClips == null || FootStepClips.Length == 0)
        {
            return;
        }

        if (Source == null)
        {
            return;
        }

        Source.pitch = 1f + Random.Range(-PitchVariation, PitchVariation);

        Source.volume = 1f + Random.Range(-VolumeVariation, VolumeVariation);

        AudioClip Clip = FootStepClips[Random.Range(0, FootStepClips.Length)];

        Source.PlayOneShot(Clip);
    }
}