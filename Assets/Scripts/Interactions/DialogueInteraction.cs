using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : Interaction {
    public string Name;

    [SerializeField]
    public List<Dial> Dialogues;
    public Destroyable DestroyableObj;

    public Enablable Enables;
    public List<Destroyable> Disables;

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

        Enables.EnableMe(true);

        if (Disables.Count > 0) {
            Destroyable sav = Disables[0];
            for(int i =1; i <Disables.Count; i++)
                Disables[i].DestroyMe(false);
            sav.DestroyMe(true);
        }
    }
}

[Serializable]
public class Dial {

    [SerializeField]
    List<Dialogue> Dialogues;

    public List<Dialogue> getDialogue() { return Dialogues; }
    public void setDialogue(List<Dialogue> d) { Dialogues = d; }
}
