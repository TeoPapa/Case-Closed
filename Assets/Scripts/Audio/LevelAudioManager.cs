using System.Collections.Generic;
using UnityEngine;

public class LevelAudioManager : AudioManager
{
    [Header("The Sound for the interaction:")]
    public AudioClip InteractionEffect;

    [Header("The Source for the Steps")]
    public AudioSource StepsSource;

    [Header("The Layer where the Ground is:")]
    public LayerMask Ground;

    string GroundName;

    [Header("Stone Steps:")]
    public List<AudioClip> CobbleSteps;

    [Header("Grass Steps:")]
    public List<AudioClip> GrassSteps;

    GameObject Player;

    private void Start() {
        Player = FindFirstObjectByType<PlayerMovement>().gameObject;
        StepsSource.playOnAwake = false;
        StepsSource.volume = GameHandler.SfxVolume;
    }

    public void Update() {
        Collider2D Collision = Physics2D.OverlapCircle(Player.transform.position, .2f, Ground);
        if (Collision == null) return;

        GroundName = Collision.tag;
    }

    public void InteractSound() {
        Sound(EffectsSource, InteractionEffect);
    }

    public void MovingSound() {

        switch (GroundName) {
            case "Cobble":
                Sound(StepsSource, CobbleSteps[UnityEngine.Random.Range(0, CobbleSteps.Count)]);
                break;

            case "Grass":
                Sound(StepsSource, GrassSteps[UnityEngine.Random.Range(0, GrassSteps.Count)]);
                break;
        }
    }
}
