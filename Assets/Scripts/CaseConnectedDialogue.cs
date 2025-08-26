using System;
using System.Collections.Generic;
using UnityEngine;

public class CaseConnectedDialogue : DialogueInteraction {
    public int LevelToMove;

    public Enablable BonusEnables;
    public List<Destroyable> BonusDisables;

    protected override void PlayerInteraction() {
        if (GameHandler.hasPlayedLevel(new Level(LevelToMove, 0)))
            IndexOfDialogue = 1;
    }

    protected override string setBubble() {
        return Name;
    }

    protected override InteractableCanvas setCanvas() {
        return FindFirstObjectByType<DialogueManager>();
    }

    protected override void EndOfInteraction() {
        if (IndexOfDialogue == 0) {
            Enables.EnableMe(true);

            if (Disables.Count > 0) {
                Destroyable sav = Disables[0];
                for (int i = 1; i < Disables.Count; i++)
                    Disables[i].DestroyMe(false);
                sav.DestroyMe(true);
            }
            return;
        }

        if(BonusEnables != null)
            BonusEnables.EnableMe(true);

        if (BonusDisables.Count > 0) {
            Destroyable sav = BonusDisables[0];
            for (int i = 1; i < BonusDisables.Count; i++)
                BonusDisables[i].DestroyMe(false);
            sav.DestroyMe(true);
        }
    }
}
