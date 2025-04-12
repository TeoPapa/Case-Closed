using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    protected Canvas myCanvas;
    protected string bubbleString;

    public abstract void PlayerInteraction();

    public string getBubble() {
        return bubbleString;
    }
}
