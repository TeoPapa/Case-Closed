using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public int DestroyableID;

    public void DestroyMe(bool save) {
        if(!GameHandler.DestroyedStuff.Contains(DestroyableID)) GameHandler.DestroyedStuff.Add(DestroyableID);

        Destroy(this.gameObject);
        if(save) GameHandler.Save(false);
    }
}
