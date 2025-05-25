using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public int DestroyableID;

    public Destroyable(int id) {
        DestroyableID = id; 
    }
    public void DestroyMe(bool save) {
        Destroy(this.gameObject);
        GameHandler.AddDestroyable(DestroyableID, save);
    }
}
