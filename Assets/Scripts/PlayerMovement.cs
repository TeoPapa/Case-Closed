using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float WalkSpeed; //The speed that the player can walk
    public bool CanMove;
    string GroundName;

    public GameObject BubbleCanvas;
    public TMP_Text MoneyValue;

    Animator PlayerAnimator;

    float GetX;
    float GetY;

    Interaction currInt;

    

    void Start() {
        PlayerAnimator = this.GetComponent<Animator>();

        BubbleCanvas.SetActive(false);
        currInt = null;
        this.gameObject.transform.position = GameHandler.PlayerPosition;

        ChangeMoney(GameHandler.Money);
        GameHandler.DestroyItems();
    }

    public void ChangeMoney(int x) {
        MoneyValue.text = x.ToString() + "$";
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

        PlayerAnimator.SetFloat("X", val.x);
        PlayerAnimator.SetFloat("Y", val.y);
        PlayerAnimator.SetBool("Moving", (val.x != 0 || val.y != 0));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.gameObject.tag.Equals("Interaction")) return;
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
        if(currInt == null) return;

        FindObjectOfType<AudioManager>().InteractSound();
        currInt.PlayerInteraction();
    }

}
