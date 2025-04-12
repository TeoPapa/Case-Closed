using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //TODO Implement Save Files For Now This Bool Will Do The Work
    public GameObject FirstTimeBtn;
    public GameObject ContinueBtn;
    public GameObject SettingsBtn;
    public GameObject Tutorial;

    public void Start() {
        int levels = GameHandler.LevelsPlayed.Count;
        bool x = levels > 0;

        FirstTimeBtn.SetActive(!x);
        ContinueBtn.SetActive(x);
        SettingsBtn.SetActive(x);
        Tutorial.SetActive(false);
    }
}
