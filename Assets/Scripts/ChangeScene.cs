using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public CaseValue CaseValue;

    public void LoadScene() {
        GameHandler.LoadScene(CaseValue);
    }
}
