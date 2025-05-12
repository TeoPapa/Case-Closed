using UnityEngine;


/* This is the Super Class that handles Interactions.
 */
public abstract class Interaction : MonoBehaviour
{
    protected InteractableCanvas Canvas; //The canvas that the interaction is connected to
    protected string bubbleString; //The message that will be shown in the information bubble

    public void Start() {
        Canvas = setCanvas();
        bubbleString = setBubble();
    }

    /* The Interaction method that is being called from the player
     */
    public void Interact() {
        PlayerInteraction();
        Canvas.Inter = this;
        Canvas.Open();
    }

    public void InteractionEnded() {
        EndOfInteraction();
    }

    /* The method that handles all operations connected to said interaction
     */
    protected virtual void PlayerInteraction() { return; }

    protected virtual void EndOfInteraction() { return; }

    /* The method that handles the setting of the appropriate canvas (e.g. Level Canvas)
     */
    protected abstract InteractableCanvas setCanvas();

    /* The method that sets the string of the bubble
     */
    protected abstract string setBubble();


    /* Returns the Name of the bubble
     */ 
    public string getBubble() {
        return bubbleString;
    }
}
