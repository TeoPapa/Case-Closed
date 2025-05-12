using System.Collections.Generic;
using UnityEngine;

public class CaseValueInteraction : Interaction
{
    public CaseValue Case;

    protected override InteractableCanvas setCanvas() {
        return FindFirstObjectByType<LevelCanvas>();
    }

    protected new void PlayerInteraction() {
        GameHandler.Case = Case;
    }

    protected new void EndOfInteraction() {
        GameHandler.Case = null;
    }

    protected override string setBubble() {
        return "Level " + Case.Level.getNumber();
    }
}
