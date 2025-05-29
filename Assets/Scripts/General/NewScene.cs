using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


/* Script that handles the Scene Changes
 */
public class NewScene : MonoBehaviour
{
    /* Change scene by name
     */
    public void ChangeScene(string Name) {
        SceneManager.LoadScene(Name);
    }

    /* Change to the DefaultScene
     */
    public void newScene() {
        if (!GameHandler.hasPlayedBefore) ChangeScene("MainMenu");
        ChangeScene(GameHandler.DefaultScene);
    }

    /* Reloads the same scene
     */
    public void reloadScene() {
        ChangeScene(SceneManager.GetActiveScene().name);
    }

}
