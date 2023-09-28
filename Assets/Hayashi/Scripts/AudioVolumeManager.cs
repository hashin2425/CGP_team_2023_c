using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeManager : MonoBehaviour
{
    public AudioSource[] managedAudioSources;

    private string seVolumeKey = "seVolume";
    private string bgmVolumeKey = "bgmVolume";
    private float seVolume;
    private float bgmVolume;
    private const float seVolumeRate = 1.00f;
    private const float bgmVolumeRate = 0.50f;

    public void updateAudioVolumes()
    {
        // BGM
        bgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey) * bgmVolumeRate;
        gameObject.GetComponent<AudioSource>().volume = bgmVolume;

        // SE
        seVolume = PlayerPrefs.GetFloat(seVolumeKey) * seVolumeRate;
        foreach (AudioSource source in managedAudioSources)
        {
            source.volume = seVolume;
        }
    }

    void Start()
    {
        updateAudioVolumes();
    }
}
