using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : Interaction {
    public string Name;

    [SerializeField]
    public List<Dial> Dialogues;
    public Destroyable DestroyableObj;

    public List<GameObject> Enables;

    int IndexOfDialogue = 0;

    protected override void PlayerInteraction() {
        if (IndexOfDialogue < 0 || IndexOfDialogue >= Dialogues.Count)
            IndexOfDialogue = 0;
    }

    protected override string setBubble() {
        return Name;
    }

    protected override InteractableCanvas setCanvas() {
        return FindFirstObjectByType<DialogueManager>();
    }

    public List<Dialogue> CurrentDialogue() {
        return Dialogues[IndexOfDialogue].getDialogue();
    }

    protected override void EndOfInteraction() {
        IndexOfDialogue++;
        if (DestroyableObj != null)
            DestroyableObj.DestroyMe(true);

        if (Enables.Count > 0) {
            foreach(GameObject e in Enables)
                e.SetActive(true);
        }
    }

    private new void Start() {
        foreach(GameObject e in Enables)
            e.gameObject.SetActive(false);

        base.Start();
    }
}

[Serializable]
public class Dial {

    [SerializeField]
    List<Dialogue> Dialogues;

    public List<Dialogue> getDialogue() { return Dialogues; }
    public void setDialogue(List<Dialogue> d) { Dialogues = d; }
}
