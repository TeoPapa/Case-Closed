using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSettings : Settings {
    public GameObject JoystickPanel;
    public GameObject ButtonsPanel;

    public GameObject TutorialJoy;
    public GameObject TutorialButton;


    new void Awake() {
        base.Awake();

        GameHandler.EnableItems();
        GameHandler.DestroyItems();
    }
    new void Start() {
        if (GameHandler.MovementMode == 1) {
            JoystickPanel.SetActive(true);
            TutorialJoy.SetActive(true);
            ButtonsPanel.SetActive(false);
        } else {
            JoystickPanel.SetActive(false);
            TutorialButton.SetActive(true);
            ButtonsPanel.SetActive(true);
        }
        base.Start();
    }

    public void ChangeMode(bool mode) {
        JoystickPanel.SetActive(mode);
        TutorialJoy.SetActive(mode);
        TutorialButton.SetActive(!mode);
        ButtonsPanel.SetActive(!mode);

        if(mode)
            GameHandler.MovementMode = 1;
        else
            GameHandler.MovementMode = 2;

        GameHandler.Save();
    }
}
