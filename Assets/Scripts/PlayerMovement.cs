using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float WalkSpeed; //The speed that the player can walk
    public bool CanMove;

    float GetX;
    float GetY;

    Interaction currInt;

    void Start() {
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
        currInt = collision.gameObject.GetComponent<Interaction>();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        currInt = null;
    }

    public void Interaction(InputAction.CallbackContext context) {
        currInt.PlayerInteraction();
    }

}
