using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Blockade : Interaction
{
    public int BlockadeCost;
    public string Description;
    public Destroyable dest;

    protected override void PlayerInteraction() {
        this.gameObject.AddComponent<Destroyable>();
        dest = this.GetComponent<Destroyable>();
        int x = 0;
        try {
            x = GameHandler.DestroyedStuff[GameHandler.DestroyedStuff.Count - 1];
        }
        catch(ArgumentOutOfRangeException e) {
            Debug.LogException(e);
        }

        Debug.Log(x);

        dest.DestroyableID = x + 1;
    }

    protected override string setBubble() {
        return "Something Blocks My Path!";
    }

    protected override InteractableCanvas setCanvas() {
        return FindFirstObjectByType<BlockCanvas>();
    }
}
