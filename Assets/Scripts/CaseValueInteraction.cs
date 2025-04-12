using System.Collections.Generic;
using UnityEngine;

public class CaseValueInteraction : Interaction
{
    public Canvas Canvas;
    public CaseValue Case;

    public void Start() {
        this.myCanvas = Canvas;
    }

    public override void PlayerInteraction() {

        Canvas.gameObject.SetActive(true);
    }

}
