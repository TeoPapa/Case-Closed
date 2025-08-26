using System;
using UnityEngine;

public abstract class InteractableCanvas : MonoBehaviour {
    public GameObject Panel;
    [HideInInspector]
    public Interaction Inter;

    private void Start() {
        Panel.SetActive(false);
        InitializeCanvas();
    }

    public void Open() {
        Panel.SetActive(true);
        FindFirstObjectByType<PlayerMovement>().SetMove(false);
        OpenCanvas();
    }

    public void Close() {
        FindFirstObjectByType<PlayerMovement>().SetMove(true);
        CloseCanvas();
        try {
            Inter.InteractionEnded();
        }
        catch (NullReferenceException e) {
            Debug.Log(e.ToString());
        }

        Inter = null;
        Panel.SetActive(false);
    }

    protected virtual void InitializeCanvas() { return; }

    protected virtual void CloseCanvas() { return; }

    protected virtual void OpenCanvas() { return; }

}
