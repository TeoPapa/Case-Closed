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

        Level thisLevel = GameHandler.LevelsPlayed.Find((Level l) => { return l.getNumber() == Case.Level.getNumber(); });
        if(thisLevel == null ) thisLevel = Case.Level;
        LevelNumber.text = thisLevel.getNumber().ToString();

        MoneyText.text = thisLevel.getMoney().ToString();

        DescriptionText.text = thisLevel.getDescription();


        Level hbp = hasBeenPlayed();

        if (!(hbp == null)) {

            for(int i = 0; i < (hbp.getMoney()/GameHandler.moneyValue); i++) {
                GameObject o = Instantiate(Lives, LivesParent.transform, LivesParent);
            }
        }
        else
            hbp = Case.Level;

        

        this.GetComponentInChildren<ChangeScene>().CaseValue = Case;
    }

    Level hasBeenPlayed() {
        Level curLvl = Case.Level;

        Level l = GameHandler.LevelsPlayed.Find( (Level l) => l.Equals(curLvl) );

        if ( l != null ) return l;

        l = new Level(-3, "");
        l.setMoney(0);
        return l;
    }
}
