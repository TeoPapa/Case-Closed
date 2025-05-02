using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public CaseValue CaseValue;

    public void LoadScene() {
        GameHandler.LoadScene(CaseValue);
    }

    public void LoadSceneWithPosition() {
        GameHandler.PlayerPosition = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        LoadScene();
    }
}
