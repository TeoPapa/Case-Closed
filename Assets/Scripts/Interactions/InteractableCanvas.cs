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
        FindFirstObjectByType<PlayerMovement>().CanMove = false;
        OpenCanvas();
    }

    public void Close() {
        FindFirstObjectByType<PlayerMovement>().CanMove = true;
        CloseCanvas();
        Inter.InteractionEnded();
        Panel.SetActive(false);
    }

    protected virtual void InitializeCanvas() { return; }

    protected virtual void CloseCanvas() { return; }

    protected virtual void OpenCanvas() { return; }

}
