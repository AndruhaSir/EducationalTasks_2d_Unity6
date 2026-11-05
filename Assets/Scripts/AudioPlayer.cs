using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void Play(AudioClip clip)
    {
        if (source == null || clip == null)
            return;

        source.PlayOneShot(clip);
    }
}
