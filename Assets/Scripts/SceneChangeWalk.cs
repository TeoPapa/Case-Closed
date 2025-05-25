using UnityEngine;

public class SceneChangeWalk : MonoBehaviour
{
    [HideInInspector]
    public NewScene NS;

    private void Awake() {
        NS = new NewScene();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        NS.ChangeScene("LevelScene");
    }
}
