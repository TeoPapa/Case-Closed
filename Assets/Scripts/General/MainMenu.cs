using TMPro;
using UnityEngine;

/* The script that handles the interactions of the Main Menu
 */
public class MainMenu : MonoBehaviour
{
    public GameObject FirstTimeBtn; //The Play button that will pop when the player plays for the first time
    public GameObject ContinueBtn; //The Continue button that shows when the player returns
    public GameObject SettingsBtn; //The Settings button that shows when the player returns
    public GameObject Tutorial; //The Tutorial panel that shows the first time a player plays (and (TODO) may be openned from settings)
    public GameObject NamePan; //The name panel

    public TMP_InputField Name; //The name input field

    public CaseValue TutorialCase; //The first case of the game

    private void Awake() {
        GameHandler.Load();
    }

    public void Start() {

        bool x = GameHandler.hasPlayedBefore;

        FirstTimeBtn.SetActive(!x);
        ContinueBtn.SetActive(x);
        SettingsBtn.SetActive(x);
        Tutorial.SetActive(false);
        NamePan.SetActive(false);

        GameHandler.Case = TutorialCase;
    }

    public void Continue(GameObject o) {
        if(GameHandler.hasPlayedBefore) {
            Tutorial.SetActive(false);
            return;
        }

        GameHandler.LoadScene();
    }

    public void NameSet() {
       string name = Name.text;
        
       if(name.Length <= 1) return;

       GameHandler.PlayerName = Name.text;
    }
}
