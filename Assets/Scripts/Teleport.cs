using UnityEngine;

public class Teleport : Interaction {
    public string TeleportName;

    public Vector2 TeleportPosition;
    public bool isInterior;
    protected override string setBubble() {
        return TeleportName;
    }

    protected override void PlayerInteraction() {
        FindFirstObjectByType<TeleportCanvas>().TeleportationSet(TeleportPosition, isInterior);
    }

    protected override InteractableCanvas setCanvas() {
        return FindFirstObjectByType<TeleportCanvas>();
    }
}
