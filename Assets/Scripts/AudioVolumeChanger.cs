using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public sealed class AudioVolumeChanger : MonoBehaviour
{
    private const float LinearVolumeFloor = 0.0001f;
    private const float DecibelsPerPowerOfTen = 20f;

    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private string exposedParameter;
    [SerializeField] private Slider volumeSlider;

    private void OnEnable()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    private void Start()
    {
        if (volumeSlider != null)
        {
            SetVolume(volumeSlider.value);
        }
    }

    private void OnDisable()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }
    }

    public void SetVolume(float linearVolume)
    {
        if (mainMixer == null || string.IsNullOrEmpty(exposedParameter))
        {
            return;
        }

        float safeVolume = Mathf.Max(linearVolume, LinearVolumeFloor);
        float decibels = Mathf.Log10(safeVolume) * DecibelsPerPowerOfTen;
        mainMixer.SetFloat(exposedParameter, decibels);
    }
}
