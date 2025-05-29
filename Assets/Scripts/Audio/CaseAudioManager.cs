using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/* This is the class that handles the Audio in the CaseScene.
 * 
 * It handles switches of music, different sounds etc.
 * 
 */
public class CaseAudioManager : AudioManager
{
    [Header("Winning Sound:")]
    public AudioClip WinningClip; //This is the music that plays when the player wins

    [Header("Losing Sound:")]
    public AudioClip LosingClip; //This is the music that plays when the player loses

    [Header("The Source for the Case sounds:")]
    public AudioSource CaseSource; //This is the AudioSource that plays sounds in parallel to
                                   //the EffectsSource

    [Header("The Clip for when changing mode:")]
    public AudioClip ModeClip; //This is the sound that plays when the player switches mode

    [Header("The Clip for the clicking of cards")]
    public AudioClip CardClick; //This is the sound that plays when the player taps one of the
                                //CaseValue items

    [Header("The Clip for when openning the case:")]
    public AudioClip CasePage; //This is the sound that plays whenever the player starts the
                               //case

    [Header("The Clips for when openning and closing the pages")]
    public List<AudioClip> PaperSounds; //These are the sounds that are played when the player
                                        //opens a tab (Location, Weapons, Items, People)

    public List<AudioClip> ClosingSounds; //These are the sounds that play when the player closes
                                          //a tab (Location, Weapon, Items, People)

    /* Initializes the CaseSource
     */
     void Start() {
        CaseSource.playOnAwake = false;
        CaseSource.volume = GameHandler.SfxVolume;
    }


    /* When the player opens the case it plays the CasePage sound
     */
    public void OpenPage() {
        Sound(CaseSource, CasePage);
    }


    /* This is the method that plays the sound when the player switches the mode
     */
    public void ChangeMode() {
        Sound(CaseSource, ModeClip);
    }

    /* This is the method that plays the CardClick sound when the player opens one of the cards
     */
    public void ClickCard() {
        Sound(CaseSource, CardClick);
    }

    /* This is the method that changes the music when the player wins or loses (x)
     */
    public void Win(bool x) {
        MusicSource.Stop(); //Stop the previous music
        MusicSource.loop = false; //And stop looping
        if (x) { //The player won
            Sound(MusicSource, WinningClip); //Play the winning music
            return;
        }

        Sound(MusicSource, LosingClip); //If he lost play the losing music
    }

    /* This is the method that handles the paper sounds. If the player Opens a tab, the game
     * will play one of the PaperSounds and when the player closes a tab, it plays one of the
     * ClosingSounds
     */
    public void PanelInteraction(bool Opens) {
        List<AudioClip> Sounds = PaperSounds;
        if(!Opens) Sounds = ClosingSounds;

        Sound(CaseSource, Sounds[UnityEngine.Random.Range(0, Sounds.Count)]);
    }
}
