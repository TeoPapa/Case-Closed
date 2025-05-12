using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CaseAudioManager : AudioManager
{
    [Header("Winning Sound:")]
    public AudioClip WinningClip;

    [Header("Losing Sound:")]
    public AudioClip LosingClip;

    [Header("The Source for the Paper sounds:")]
    public AudioSource CaseSource;

    [Header("The Clip for when changing mode:")]
    public AudioClip ModeClip;

    [Header("The Clip for the clicking of cards")]
    public AudioClip CardClick;

    [Header("The Clip for when openning the case:")]
    public AudioClip CasePage;

    [Header("The Clips for when openning and closing the pages")]
    public List<AudioClip> PaperSounds;
    public List<AudioClip> ClosingSounds;

     void Start() {
        CaseSource.playOnAwake = false;
        CaseSource.volume = GameHandler.SfxVolume;
    }

    public void OpenPage() {
        Sound(CaseSource, CasePage);
    }

    public void ChangeMode() {
        Sound(CaseSource, ModeClip);
    }

    public void ClickCard() {
        Sound(CaseSource, CardClick);
    }

    public void Win(bool x) {
        MusicSource.Stop();
        MusicSource.loop = false;
        if (x) {
            Sound(MusicSource, WinningClip);
            return;
        }

        Sound(MusicSource, LosingClip);
    }



    public void PanelInteraction(bool Opens) {
        List<AudioClip> Sounds = PaperSounds;
        if(!Opens) Sounds = ClosingSounds;

        Sound(CaseSource, Sounds[UnityEngine.Random.Range(0, Sounds.Count)]);
    }
}
