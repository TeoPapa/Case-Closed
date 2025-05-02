using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource MusicSource;

    public AudioSource EffectsSource;

    public AudioSource StepsSource;

    public LayerMask Ground;
    string GroundName;

    public AudioClip InteractionEffect;
    public AudioClip ClickingEffect;

    public List<AudioClip> CobbleSteps;
    public List<AudioClip> GrassSteps;

    public void Start() {
        Button[] btns = FindObjectsOfType<Button>();

        foreach(Button b in btns) {
            if(b.gameObject.tag != "UserInterfaceButtons")
                b.onClick.AddListener(Click);
        }
    }

    public void Update() {
        Collider2D Collision = Physics2D.OverlapCircle(this.gameObject.transform.position, .2f, Ground);
        if (Collision == null) return;

        GroundName = Collision.tag;
    }

    public void Click() {
        Sound(EffectsSource, ClickingEffect);
    }

    public void InteractSound() {
        Sound(EffectsSource, InteractionEffect);
    }

    public void MovingSound() {

        switch(GroundName) {
            case "Cobble":
                Sound(StepsSource, CobbleSteps[UnityEngine.Random.Range(0, CobbleSteps.Count)]);
                break;

            case "Grass":
                Sound(StepsSource, GrassSteps[UnityEngine.Random.Range(0, GrassSteps.Count)]);
                break;
        }
    }

    void Sound(AudioSource source, AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
}
