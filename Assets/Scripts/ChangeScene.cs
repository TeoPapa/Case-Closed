using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{

    public void LoadScene(CaseValue c) {
        GameHandler.Case = c;
        GameHandler.LoadScene();
    }

    public void LoadSceneWithPosition(CaseValue c) {
        GameHandler.PlayerPosition = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        LoadScene(c);
    }

    public void LoadSceneWithPosition() {
        GameHandler.PlayerPosition = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        LoadScene(GameHandler.Case);
    }
}
