using UnityEngine;

public class WardrobeInteraction : Interaction {
    protected override string setBubble() {
        return "Wardrobe";
    }

    protected override InteractableCanvas setCanvas() {
        return FindFirstObjectByType<WardrobeCanvas>();
    }
}
