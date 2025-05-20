using System.Collections.Generic;
using UnityEngine;

public class OneTimeDialogue : MonoBehaviour
{
    [SerializeField]
    public Dial Dialogue;
    public Destroyable DestroyableObj;

    public List<Destroyable> OthersToDestroy;

    private void OnTriggerEnter2D(Collider2D collision) {
        FindFirstObjectByType<DialogueManager>().OpenCanvas(Dialogue.getDialogue());
        foreach(Destroyable obj in OthersToDestroy)
            obj.DestroyMe(false);
       
        DestroyableObj.DestroyMe(true);
    }
}
