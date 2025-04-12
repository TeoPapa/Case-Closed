using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float WalkSpeed; //The speed that the player can walk
    public bool CanMove;

    public GameObject BubbleCanvas;

    float GetX;
    float GetY;

    Interaction currInt;

    

    void Start() {
        BubbleCanvas.SetActive(false);
        currInt = null;
        this.gameObject.transform.position = GameHandler.PlayerPosition;
    }

    private void Update() {
        if(!CanMove) {
            GetX = 0;
            GetY = 0;
        } 
    }

    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(GetX * WalkSpeed, GetY * WalkSpeed);
    }

    public void Move(InputAction.CallbackContext context) {
        Vector2 val = new Vector2(0,0);
        if (CanMove)
            val = context.ReadValue<Vector2>();
        
        GetX = val.x;
        GetY = val.y;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        TMP_Text txt = BubbleCanvas.GetComponentInChildren<TMP_Text>();
        currInt = collision.gameObject.GetComponent<Interaction>();

        txt.text = currInt.getBubble();

        BubbleCanvas.SetActive(true);

        currInt = collision.gameObject.GetComponent<Interaction>();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        currInt = null;
        BubbleCanvas.SetActive(false);
    }

    public void Interaction() {
        currInt.PlayerInteraction();
    }

}
