using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("The Source for the Music:")]
    public AudioSource MusicSource;

    [Header("Music Clip:")]
    public AudioClip Music;

    [Header("The Source for the effects:")]
    public AudioSource EffectsSource;

    [Header("Clicking Sound:")]
    public AudioClip ClickingEffect;

    protected void Awake() {
        MusicSource.loop = true;
        MusicSource.clip = Music;
        MusicSource.volume = GameHandler.MusicVolume;
        MusicSource.playOnAwake = true;

        Button[] btns = FindObjectsByType<Button>(FindObjectsSortMode.None);

        foreach(Button b in btns) {
            if(b.gameObject.tag != "UserInterfaceButtons")
                b.onClick.AddListener(Click);
        }
        EffectsSource.playOnAwake = false;
        EffectsSource.volume = GameHandler.SfxVolume;
    }

    public void Click() {
        Sound(EffectsSource, ClickingEffect);
    }
    public void setMusicVolume() {
        MusicSource.volume = GameHandler.MusicVolume;
    }

    protected void Sound(AudioSource source, AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
}
