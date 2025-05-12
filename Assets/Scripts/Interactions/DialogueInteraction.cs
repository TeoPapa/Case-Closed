using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : Interaction {
    public string Name;

    [SerializeField]
    public List<Dial> Dialogues;
    public Destroyable DestroyableObj;

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
    }
}

[Serializable]
public class Dial {

    [SerializeField]
    List<Dialogue> Dialogues;

    public List<Dialogue> getDialogue() { return Dialogues; }
    public void setDialogue(List<Dialogue> d) { Dialogues = d; }
}
