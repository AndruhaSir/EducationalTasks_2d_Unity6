using UnityEngine;
using UnityEngine.UI;

public sealed class AudioMuteController : MonoBehaviour
{
    private const float MutedListenerVolume = 0f;
    private const float NormalListenerVolume = 1f;

    [SerializeField] private Toggle muteToggle;

    private void OnEnable()
    {
        if (muteToggle != null)
        {
            muteToggle.onValueChanged.AddListener(SetMuted);
        }
    }

    private void Start()
    {
        if (muteToggle != null)
        {
            SetMuted(muteToggle.isOn);
        }
    }

    private void OnDisable()
    {
        if (muteToggle != null)
        {
            muteToggle.onValueChanged.RemoveListener(SetMuted);
        }
    }

    private void SetMuted(bool isMuted)
    {
        AudioListener.volume = isMuted ? MutedListenerVolume : NormalListenerVolume;
    }
}
