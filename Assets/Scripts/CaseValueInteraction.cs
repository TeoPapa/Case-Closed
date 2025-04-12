using System.Collections.Generic;
using UnityEngine;

public class CaseValueInteraction : Interaction
{
    public CaseValue Case;
    LevelCanvas c;

    public void Start() {
        c = FindObjectOfType<LevelCanvas>();
        this.bubbleString = "Level " + Case.Level.getNumber();
    }

    public override void PlayerInteraction() {
        c.Case = Case;
        c.Open();
    }

}
