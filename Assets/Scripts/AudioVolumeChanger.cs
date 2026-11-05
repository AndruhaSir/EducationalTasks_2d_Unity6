using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeChanger : MonoBehaviour
{
    private const float LinearVolumeFloor = 0.0001f;
    private const float MutedDecibels = -80f;
    private const float DecibelsPerPowerOfTen = 20f;

    [Header("Mixer")]
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private string masterParameter = "MasterParameter";
    [SerializeField] private string sfxParameter = "SFXParameter";
    [SerializeField] private string musicParameter = "MusicParameter";

    [Header("UI")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle muteToggle;

    private bool _isMuted;
    private float _lastMasterVolume = 1f;

    private void OnEnable()
    {
        if (masterSlider != null)
            masterSlider.onValueChanged.AddListener(SetMasterVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(SetMusicVolume);

        if (muteToggle != null)
            muteToggle.onValueChanged.AddListener(ToggleMute);
    }

    private void Start()
    {
        if (masterSlider != null) SetMasterVolume(masterSlider.value);
        if (sfxSlider != null) SetSFXVolume(sfxSlider.value);
        if (musicSlider != null) SetMusicVolume(musicSlider.value);
        if (muteToggle != null) ToggleMute(muteToggle.isOn);
    }

    private void OnDisable()
    {
        if (masterSlider != null) masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
        if (musicSlider != null) musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        if (muteToggle != null) muteToggle.onValueChanged.RemoveListener(ToggleMute);
    }

    public void SetMasterVolume(float value)
    {
        _lastMasterVolume = value;
        if (!_isMuted) SetMixerVolume(masterParameter, value);
    }

    public void SetSFXVolume(float value) => SetMixerVolume(sfxParameter, value);

    public void SetMusicVolume(float value) => SetMixerVolume(musicParameter, value);

    public void ToggleMute(bool value)
    {
        _isMuted = value;
        SetMixerDecibels(masterParameter, value ? MutedDecibels : ToDecibels(_lastMasterVolume));
    }

    private void SetMixerVolume(string parameter, float linearVolume)
    {
        SetMixerDecibels(parameter, ToDecibels(linearVolume));
    }

    private void SetMixerDecibels(string parameter, float decibels)
    {
        if (mainMixer != null && !string.IsNullOrEmpty(parameter))
            mainMixer.SetFloat(parameter, decibels);
    }

    private static float ToDecibels(float linearVolume)
    {
        return Mathf.Log10(Mathf.Max(linearVolume, LinearVolumeFloor)) * DecibelsPerPowerOfTen;
    }
}
