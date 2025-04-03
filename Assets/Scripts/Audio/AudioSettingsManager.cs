using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sfxSlider;
    public Slider envSlider;

    void Start()
    {
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        envSlider.onValueChanged.AddListener(SetEnvVolume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); 
    }

    public void SetEnvVolume(float volume)
    {
        audioMixer.SetFloat("EnvVolume", Mathf.Log10(volume) * 20);
    }
}
