using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance {  get; private set; }

    const string Player_Pref_Volume_Music = "PlayerPrefVolume";

    float volume = .3f;
    AudioSource musicSource;


    private void Awake()
    {
        instance = this;
        musicSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(Player_Pref_Volume_Music, volume);
        musicSource.volume = volume;

    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }

        musicSource.volume = volume;
        PlayerPrefs.SetFloat(Player_Pref_Volume_Music, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }

}
