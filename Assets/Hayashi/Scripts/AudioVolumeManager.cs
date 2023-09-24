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

    public void updateAudioVolumes()
    {
        // BGM
        bgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey);
        gameObject.GetComponent<AudioSource>().volume = bgmVolume;

        // SE
        seVolume = PlayerPrefs.GetFloat(seVolumeKey);
        foreach (AudioSource source in managedAudioSources)
        {
            source.volume = bgmVolume;
        }
    }

    void Start()
    {
        updateAudioVolumes();
    }
}
