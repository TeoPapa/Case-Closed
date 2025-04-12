using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    public void ChangeScene(string Name) {
        SceneManager.LoadScene(Name);
    }

    public void newScene() {
        if (!GameHandler.hasPlayedBefore) ChangeScene("MainMenu");
        ChangeScene(GameHandler.DefaultScene);
    }

    public void reloadScene() {
        ChangeScene(SceneManager.GetActiveScene().name);
    }

}
