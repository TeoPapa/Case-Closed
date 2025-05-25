using System.Collections.Generic;
using UnityEngine;

public class OneTimeDialogue : MonoBehaviour
{
    [SerializeField]
    public Dial Dialogue;
    public int DestroyableID;

    public List<Destroyable> OthersToDestroy;

    private void Awake() {
        this.gameObject.AddComponent<Destroyable>();
        OthersToDestroy.Add(this.GetComponent<Destroyable>());
        OthersToDestroy[OthersToDestroy.Count - 1].DestroyableID = DestroyableID;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if( GameHandler.isDestroyed(DestroyableID)) return;

        FindFirstObjectByType<DialogueManager>().OpenCanvas(Dialogue.getDialogue());

        for(int i = 1; i < OthersToDestroy.Count; i++)
            OthersToDestroy[i].DestroyMe(false);

        OthersToDestroy[0].DestroyMe(true);
    }
}
