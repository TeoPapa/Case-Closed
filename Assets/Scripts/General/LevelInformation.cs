using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInformation : MonoBehaviour
{
    public TMP_Text LevelNumber;

    public GameObject LivesParent;

    public GameObject Lives;

    [HideInInspector]
    public CaseValue Val;

    public void SetLevel(int Number, int NumberOfLives, CaseValue Case) {
        LevelNumber.text = Number.ToString();
        Val = Case;

        for (int i = 0; i < NumberOfLives && i < 3; i++) {
            GameObject o = Instantiate(Lives, LivesParent.transform, LivesParent);
        }
    }

    public void OpenCase() {
        GameHandler.Case = Val;
        FindAnyObjectByType<LevelCanvas>().OpenCanvas(Val);
    }
}

