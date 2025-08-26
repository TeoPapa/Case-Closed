using UnityEngine;

public class MapInteraction : Interaction {
    protected override string setBubble() {
        return "Map";
    }

    protected override InteractableCanvas setCanvas() {
        return FindFirstObjectByType<WorldMap>();
    }
}
