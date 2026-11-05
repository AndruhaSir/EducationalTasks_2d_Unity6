using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioButton : MonoBehaviour
{
    [SerializeField] private AudioPlayer audioPlayer;
    [SerializeField] private AudioClip clip;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (_button == null)
        {
            _button = GetComponent<Button>();
        }

        _button.onClick.AddListener(PlayClip);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlayClip);
    }

    private void PlayClip()
    {
        if (audioPlayer != null)
        {
            audioPlayer.Play(clip);
        }
    }
}