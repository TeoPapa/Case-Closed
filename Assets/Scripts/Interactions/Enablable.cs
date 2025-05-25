using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Enablable : MonoBehaviour
{
    public int EnableID;

    public List<GameObject> Enables;

    public void EnableMe(bool Save) {
        foreach (GameObject en in Enables)
            en.SetActive(true);

        GameHandler.AddEnablable(EnableID, Save);

        if (Save) GameHandler.Save(false);
    }

    private void OnEnable() {
        if(GameHandler.isEnabled(EnableID)) return;

        foreach (GameObject en in Enables)
            en.SetActive(false);
    }
}
