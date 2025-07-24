using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public List<GameObject> PopUps;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (GameHandler.isDestroyed(this.GetComponent<Destroyable>().DestroyableID)) return;

        foreach (GameObject go in PopUps)
            go.SetActive(true);

        GetComponent<Destroyable>().DestroyMe(true);

        FindFirstObjectByType<PlayerMovement>().SetMove(false);
    }
}
