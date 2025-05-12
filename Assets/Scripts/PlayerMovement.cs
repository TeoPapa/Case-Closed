using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


/* This is the class that handles the Player Movement and Player Interactions
 */
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; //The body of the player

    public float WalkSpeed; //The speed that the player can walk
    public bool CanMove; //If the player can walk (is in interaction)
    string GroundName; //What is the current ground the player is walking on

    public GameObject BubbleCanvas; //The GameObject where the information pops 
    public TMP_Text MoneyValue; //The information of the interaction (e.g. Level
                                //number, dialogue pop ups etc.

    public GameObject TutorialCanvas;

    Animator PlayerAnimator; //The Player's Animator

    float GetX; //Variables that hold the current
    float GetY; //movement values of X and Y


    Interaction currInt; //The current interaction that the player can interact with


    void Start() {
        Debug.Log(GameHandler.hasPlayedBefore);

        if(!GameHandler.hasPlayedBefore) TutorialCanvas.SetActive(true);

        PlayerAnimator = this.GetComponent<Animator>(); //Initializes the Animator with the Player's component

        BubbleCanvas.SetActive(false);
        currInt = null;

        this.gameObject.transform.position = GameHandler.PlayerPosition; //Sets the player position accordingly to GameHandler

        ChangeMoney(GameHandler.Money); //Shows in UI how much money the player has
        GameHandler.DestroyItems(); //Destroys all already interacted destroyable items
    }

    private void FixedUpdate() {
        rb.linearVelocity = new Vector2(GetX * WalkSpeed, GetY * WalkSpeed);
    }

    /* Sets the UI text money to the x ammount (Shows: x$ )
     */
    public void ChangeMoney(int x) {
        MoneyValue.text = x.ToString() + "$";
    }


    public void Move(InputAction.CallbackContext context) {
        Vector2 val = new Vector2(0,0);
        GetX = GetY = 0;

        if (CanMove)
            val = context.ReadValue<Vector2>();

        if (Mathf.Abs(val.x) > Mathf.Abs(val.y))
            GetX = val.x;
        else
            GetY = val.y;

        PlayerAnimator.SetFloat("X", GetX);
        PlayerAnimator.SetFloat("Y", GetY);
        PlayerAnimator.SetBool("Moving", (GetX != 0 || GetY != 0));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.gameObject.tag.Equals("Interaction")) return;


        BubbleCanvas.SetActive(true);
        TMP_Text txt = BubbleCanvas.GetComponentInChildren<TMP_Text>();
        currInt = collision.gameObject.GetComponent<Interaction>();

        txt.text = currInt.getBubble();


        currInt = collision.gameObject.GetComponent<Interaction>();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        currInt = null;
        BubbleCanvas.SetActive(false);
    }

    public void Interaction() {
        if(currInt == null) return;

        FindFirstObjectByType<LevelAudioManager>().InteractSound();
        currInt.Interact();
    }

    public void Step() {
        FindFirstObjectByType<LevelAudioManager>().MovingSound();
    }
}
