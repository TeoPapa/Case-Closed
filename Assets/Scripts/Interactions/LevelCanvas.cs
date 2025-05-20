using System;
using TMPro;
using UnityEngine;

public class LevelCanvas : InteractableCanvas {

    public TMP_Text LevelNumber;
    public TMP_Text MoneyText;
    public TMP_Text DescriptionText;

    public GameObject LivesParent;
    public GameObject Lives;

    Level hasBeenPlayed(Level curLvl) {

        Level l = GameHandler.LevelsPlayed.Find( (Level l) => l.Equals(curLvl) );

        if ( l != null ) return l;

        l = new Level(-3, "");
        l.setMoney(0);
        return l;
    }

    protected override void CloseCanvas() {
        GameHandler.Case = null;

        foreach (Transform child in LivesParent.transform)
            Destroy(child.gameObject);
    }

    protected override void OpenCanvas() {
        CaseValueInteraction c = (CaseValueInteraction)Inter;
        CaseValue Case = c.Case;

        Level thisLevel = GameHandler.LevelsPlayed.Find((Level l) => { return l.getNumber() == Case.Level.getNumber(); });
        if (thisLevel == null) thisLevel = Case.Level;
        LevelNumber.text = thisLevel.getNumber().ToString();

        MoneyText.text = thisLevel.getMoney().ToString();

        DescriptionText.text = thisLevel.getDescription();


        Level hbp = hasBeenPlayed(Case.Level);

        if (!(hbp == null)) {

            for (int i = 0; i < (hbp.getMoney() / GameHandler.moneyValue); i++) {
                GameObject o = Instantiate(Lives, LivesParent.transform);
            }
        } else
            hbp = Case.Level;

        GameHandler.Case = Case;
    }
}
