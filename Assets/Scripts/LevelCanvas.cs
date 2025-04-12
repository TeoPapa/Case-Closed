using System;
using TMPro;
using UnityEngine;

public class LevelCanvas : MonoBehaviour {

    public GameObject Panel;
    public TMP_Text LevelNumber;
    public TMP_Text MoneyText;
    public TMP_Text DescriptionText;

    public GameObject LivesParent;
    public GameObject Lives;

    [HideInInspector]
    public CaseValue Case;

    private void Start() {
        Panel.SetActive(false);
    }

    public void Open() {
        Panel.SetActive(true);

        Level hbp = hasBeenPlayed();

        if (!(hbp == null)) {
            LevelNumber.text = hbp.getNumber().ToString();

            for(int i = 3; i > (hbp.getMoney()/GameHandler.moneyValue); i--) {
                GameObject o = Instantiate(Lives, LivesParent.transform, LivesParent);
            }

            MoneyText.text = hbp.getMoney().ToString();

            DescriptionText.text = hbp.getDescription();
        }

        this.GetComponentInChildren<ChangeScene>().CaseValue = Case;
    }

    Level hasBeenPlayed() {
        Level curLvl = Case.Level;

        Predicate<Level> lvl = (Level l) => {
            return l.getNumber() == curLvl.getNumber() && l.getDescription() == curLvl.getDescription();
        };

        if(lvl != null)
            return GameHandler.LevelsPlayed.Find(lvl);

        return null;
    }
}
