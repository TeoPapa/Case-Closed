using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInformation : MonoBehaviour
{
    public TMP_Text LevelNumber;

    public GameObject LivesParent;

    public GameObject Lives;

    public void SetLevel(int Number, int NumberOfLives) {
        LevelNumber.text = Number.ToString();

        for (int i = 0; i < NumberOfLives && i < 3; i++) {
            GameObject o = Instantiate(Lives, LivesParent.transform, LivesParent);
        }
    }
}

