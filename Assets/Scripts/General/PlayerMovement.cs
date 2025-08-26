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

    public PlayerInstanceValues Interior;
    public PlayerInstanceValues Exterior;

    public GameObject BubbleCanvas; //The GameObject where the information pops 
    public RectTransform BubbleTransform;
    public TMP_Text MoneyValue; //The information of the interaction (e.g. Level
                                //number, dialogue pop ups etc.

    public Animator PlayerAnimator; //The Player's Animator

    float GetX; //Variables that hold the current
    float GetY; //movement values of X and Y


    Interaction currInt; //The current interaction that the player can interact with

    public void ChangePlayer(bool isInterior) {
        GameHandler.IsInside = isInterior;
        if (isInterior) {//Goes in interior
            ChangeMovementValues(Interior.Scale, Interior.Speed, Interior.Size, Interior.Bubble);
            FindFirstObjectByType<ObjectiveTrack>().Activate(false);
        } else {//Goes to city
            ChangeMovementValues(Exterior.Scale, Exterior.Speed, Exterior.Size, Exterior.Bubble);
            FindFirstObjectByType<ObjectiveTrack>().Activate(true);
        }
    }

    void Start() {
        BubbleCanvas.SetActive(false);
        currInt = null;

        this.gameObject.transform.position = GameHandler.PlayerPosition; //Sets the player position accordingly to GameHandler
        ChangePlayer(GameHandler.IsInside);
        ChangeMoney(GameHandler.Money); //Shows in UI how much money the player has
    }

    private void FixedUpdate() {
        if (CanMove) {
            rb.linearVelocity = new Vector2(GetX * WalkSpeed, GetY * WalkSpeed);
        } else {
            rb.linearVelocity = new Vector2(0, 0);
        }
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
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!collision.gameObject.tag.Equals("Interaction")) return;

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

    public void ChangeMovementValues(float Scale,float Speed, float CameraSize, Vector3 Bubble) {
        BubbleCanvas.SetActive(true);

        BubbleTransform.anchoredPosition = new Vector3(Bubble.x, Bubble.y, Bubble.z);

        transform.localScale = new Vector3(Scale, Scale, 1);
        WalkSpeed = Speed;
        GetComponentInChildren<Camera>().orthographicSize = CameraSize;

        BubbleCanvas.SetActive(false);
    }

    public void SetMove(bool cm) {
        CanMove = cm;
    }
}
