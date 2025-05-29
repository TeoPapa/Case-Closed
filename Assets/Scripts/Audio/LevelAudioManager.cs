using System.Collections.Generic;
using UnityEngine;


/* This is the class that handles the sounds in the LevelScene (the scene where the player
 * traverses in the city and interacts with NPCs, Cases etc).
 * 
 * It uses as it's base the AudioManager class
 */
public class LevelAudioManager : AudioManager
{
    [Header("The Sound for the interaction:")]
    public AudioClip InteractionEffect; //This is the sound effect that plays when the player
                                        //interacts with the world.
    [Header("The Source for the Steps")]
    public AudioSource StepsSource; //This is the AudioSource that plays the Player's stepping
                                    //sounds

    [Header("The Layer where the Ground is:")]
    public LayerMask Ground; //This is the Layer that has all the GameObjects that are a part
                             //of the ground

    [Header("Stone Steps:")]
    public List<AudioClip> CobbleSteps; //The sounds that play when the player is on Cobble-
                                        //stone ground

    [Header("Grass Steps:")]
    public List<AudioClip> GrassSteps; //The sounds that play whenever the player steps on grass

    [Header("Wood Steps:")]
    public List<AudioClip> WoodSteps; //The sounds that play when the player walks on wood

    [Header("Sand Steps:")]
    public List<AudioClip> SandSteps; //The sounds that play whenever the player is on sand

    GameObject Player; //The GameObject of the  Player

    string GroundName; //A variable that knows in what type of ground the player is on

    /* This method initializes LevelAudioManager by setting the Player object to the player's
     * GameObject and sets Steps source volume and playOnAwake values
     */
    private void Start() {
        Player = FindFirstObjectByType<PlayerMovement>().gameObject;
        StepsSource.playOnAwake = false;
        StepsSource.volume = GameHandler.SfxVolume;
    }


    /* In each update this AudioManager checks in what type of ground the player is currently on.
     */
    public void Update() {
        Collider2D Collision = Physics2D.OverlapCircle(Player.transform.position, .2f, Ground);
        if (Collision == null) return;

        GroundName = Collision.tag;
    }


    /* The method that plays the Interaction sound when the player interacts
     */
    public void InteractSound() {
        Sound(EffectsSource, InteractionEffect);
    }


    /* Whenever the player makes a step, they call the MovingSound method that plays one of the
     * GroundName (accordingly to what type of ground the player is on) sounds, chose randomly
     * from the Lists.
     */
    public void MovingSound() {

        switch (GroundName) {
            case "Cobble":
                Sound(StepsSource, CobbleSteps[UnityEngine.Random.Range(0, CobbleSteps.Count)]);
                break;

            case "Grass":
                Sound(StepsSource, GrassSteps[UnityEngine.Random.Range(0, GrassSteps.Count)]);
                break;

            case "Wood":
                Sound(StepsSource, WoodSteps[UnityEngine.Random.Range(0, WoodSteps.Count)]);
                break;
            case "Sand":
                Sound(StepsSource, SandSteps[UnityEngine.Random.Range(0, SandSteps.Count)]);
                break;
        }
    }
}
