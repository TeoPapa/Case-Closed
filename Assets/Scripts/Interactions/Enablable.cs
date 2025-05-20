using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Enablable : MonoBehaviour
{
    public int EnableID;
    public List<GameObject> Enables;

    private void Awake() {
        foreach(GameObject o in Enables)
            o.SetActive(false);
    }

    public void EnableMe(bool Save) {
        if (!GameHandler.EnabledStuff.Contains(EnableID)) GameHandler.DestroyedStuff.Add(EnableID);

        foreach (GameObject en in Enables)
            en.SetActive(true);

        if (Save) GameHandler.Save(false);
    }
}
