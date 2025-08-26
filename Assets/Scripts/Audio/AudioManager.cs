using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


/* This is the base class of the Audio classes for the game. It handles
 * all the sounds that happen in the different instances of the game.
 * 
 * In this form it handles the music and one time sound effects like tap
 * sounds etc.
 */
public class AudioManager : MonoBehaviour
{
    [Header("The Source for the Music:")]
    public AudioSource MusicSource; //This is the AudioSource that handles
                                    //the music

    [Header("Music Clip:")]
    public AudioClip Music; //This is the clip that has the background music

    [Header("The Source for the effects:")]
    public AudioSource EffectsSource; //This is the AudioSource that handles
                                      //the sound effects

    [Header("Clicking Sound:")]
    public AudioClip ClickingEffect; //This is the sound effect when tapping buttons


    /* In this method the AudioManager initiazes all sound sources (seeting their
     * volume levels according to the save, setting the loops etc.) and also adds the
     * tapping sound to every button that isn't a part of the interface.
     */
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

        MusicSource.Play();
    }

    /* This is the listener for the UI buttons of the game. This Listener is added
     * in the Awake method of this class
     */
    public void Click() {
        Sound(EffectsSource, ClickingEffect);
    }

    /* This is the method that sets the volume of music
     */
    public void setMusicVolume() {
        MusicSource.volume = GameHandler.MusicVolume;
    }


    /* This method plays an AudioClip clip to the source AudioSource
     */
    protected void Sound(AudioSource source, AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
}
