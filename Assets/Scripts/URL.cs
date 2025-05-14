using UnityEngine;

public class URL : MonoBehaviour
{

    public string url;

    public void openUrl() {
        Application.OpenURL(url);
    }

    public void openUrl(string url) {
        Application.OpenURL(url);
    }
}
