using System.Collections.Generic;
using UnityEngine;

public class OneTimeDialogue : MonoBehaviour
{
    [SerializeField]
    public Dial Dialogue;
    public Destroyable DestroyableObj;

    private void OnTriggerEnter2D(Collider2D collision) {
        FindFirstObjectByType<DialogueManager>().OpenCanvas(Dialogue.getDialogue());
        DestroyableObj.DestroyMe(true);
    }
}
