using UnityEngine;

public class LevelSettings : Settings {
    public GameObject JoystickPanel;
    public GameObject ButtonsPanel;

    public GameObject TutorialJoy;
    public GameObject TutorialButton;

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
    }
}
