using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CaseHandler : MonoBehaviour {

    public bool Mode = false; //TRUE is on detective mode (says that an item belongs in the case), FALSE is on questionare mode (is questioning suspects and investigating)

    [Header("The Prefab With The Case Button")]
    public GameObject CaseItemPrefab;

    [Header("The Text Box With The Case Number")]
    public TMP_Text CaseNum;

    [Header("The Text Box With The Description")]
    public TMP_Text Description;

    [Header("The Buttons Of The Case")]
    public GameObject LocationBtn;
    public GameObject WeaponBtn;
    public GameObject StolenBtn;
    public GameObject PeopleBtn;

    [Header("The Panels Of Each Button")]
    public GameObject LocationPanel;
    public GameObject WeaponPanel;
    public GameObject StolenPanel;
    public GameObject PeoplePanel;

    [Header("The Parent Of Each Page")]
    public GameObject LocationParent;
    public GameObject WeaponParent;
    public GameObject StolenParent;
    public GameObject PeopleParent;

    [Header("The Objects Of Each Item")]
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;
    int Lives = 3;

    [Header("The Ready And Investigation Buttons")]
    public GameObject ReadyModebtn;
    public GameObject InvestigationModebtn;
    public Color ActiveColor;
    public Color DeactiveColor;

    [Header("The End Screen")]
    public GameObject EndScreen;

    [Header("The Winning Screen")]
    public GameObject WinScreen;

    public GameObject HintsPrefab;
    public GameObject HintsParent;

    public GameObject MoneyRec;

    [Header("All The Objectives Text")]
    public GameObject allobjtxt;

    [Header("Currently Found Objectives Text")]
    public GameObject currFoundtxt;

    public GameObject AllItems;
    public GameObject cuurentItems;


    int AllTheObjects = 0;
    int Everything = 0;

    public int FoundObjectives;
    public int FoundItems;

    public void Start() {
        EndScreen.SetActive(false);
        WinScreen.SetActive(false);

        currFoundtxt.GetComponent<TMP_Text>().text = "0";
        cuurentItems.GetComponent<TMP_Text>().text = "0";

        LocationBtn.SetActive(true);
        WeaponBtn.SetActive(true);
        StolenBtn.SetActive(true);
        PeopleBtn.SetActive(true);

        LocationPanel.SetActive(true);
        WeaponPanel.SetActive(true);
        StolenPanel.SetActive(true);
        PeoplePanel.SetActive(true);


        CaseNum.text = GameHandler.Case.Level.ToString();
        Description.text = GameHandler.Case.Description;

        Life1.SetActive(true);
        Life2.SetActive(true);
        Life3.SetActive(true);

        foreach (CaseItemType i in GameHandler.Case.Location) {
            Everything++;
            if(i.isInCase())
                AllTheObjects++;

            GameObject o = Instantiate(CaseItemPrefab, LocationParent.transform, LocationParent);
            o.GetComponent<CaseItem>().ObjectCreated(i);
        }

        foreach (CaseItemType i in GameHandler.Case.Weapons) {
            Everything++;
            if (i.isInCase())
                AllTheObjects++;

            GameObject o = Instantiate(CaseItemPrefab, WeaponParent.transform, WeaponParent);
            o.GetComponent<CaseItem>().ObjectCreated(i);
        }

        foreach (CaseItemType i in GameHandler.Case.StolenItems) {
            Everything++;
            if (i.isInCase())
                AllTheObjects++;

            GameObject o = Instantiate(CaseItemPrefab, StolenParent.transform, StolenParent);
            o.GetComponent<CaseItem>().ObjectCreated(i);
        }

        foreach (CaseItemType i in GameHandler.Case.People) {
            Everything++;
            if (i.isInCase())
                AllTheObjects++;

            GameObject o = Instantiate(CaseItemPrefab, PeopleParent.transform, PeopleParent);
            o.GetComponent<CaseItem>().ObjectCreated(i);
        }

        LocationPanel.SetActive(false);
        WeaponPanel.SetActive(false);
        StolenPanel.SetActive(false);
        PeoplePanel.SetActive(false);

       allobjtxt.GetComponent<TMP_Text>().text = AllTheObjects.ToString();
       AllItems.GetComponent<TMP_Text>().text = Everything.ToString();

        ChangeColors(Mode);

        if (GameHandler.Case.Location.Count <= 0) LocationBtn.SetActive(false);

        if (GameHandler.Case.Weapons.Count <= 0) WeaponBtn.SetActive(false);

        if (GameHandler.Case.StolenItems.Count <= 0) StolenBtn.SetActive(false);

        if (GameHandler.Case.People.Count <= 0) PeopleBtn.SetActive(false);
    }
        
    public void switchMode(bool mode) {
        Mode = mode;
        ChangeColors(Mode);
    }

    void ChangeColors(bool Type) {
        Image ReadyBtn = ReadyModebtn.GetComponent<Image>();
        Image InvBtn = InvestigationModebtn.GetComponent<Image>();

        if (!Type) {
            ReadyBtn.color = DeactiveColor;
            InvBtn.color = ActiveColor;
            return;
        }

        InvBtn.color = DeactiveColor;
        ReadyBtn.color = ActiveColor;
    }

    ColorBlock ChangeColorToBtn(Color c) {
        ColorBlock cb = new ColorBlock();
        cb.highlightedColor = c;
        cb.selectedColor = c;
        cb.normalColor = c;
        cb.pressedColor = c;
        return cb;
    }

    public void loseLife() {
    
        switch(Lives) {
            case 3:
                Life3.SetActive(false);
                break;
            case 2:
                Life2.SetActive(false);
                break;
            case 1:
                Life1.SetActive(false);
                break;
            default:
                GameLost();
                break;
        }

        Lives -= 1;
        Handheld.Vibrate();
    }

    public void GameLost() {
        EndScreen.SetActive(true);
    }

    public void FoundItem(bool b) {

        FoundItems += 1;
        if (b) FoundObjectives += 1;

        currFoundtxt.GetComponent<TMP_Text>().text = FoundObjectives.ToString();
        cuurentItems.GetComponent<TMP_Text>().text = FoundItems.ToString();


        if (b != Mode) {
            loseLife();
            return;
        }

        if (FoundItems == Everything) {
            StopAllCoroutines();
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter() {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(.5f);


        GameWon();
    }

    void GameWon() {
        WinScreen.SetActive(true);

        for (int i = 0; i < Lives; i++) {
            GameObject o = Instantiate(HintsPrefab, HintsParent.transform, HintsParent);
        }

        int gain = GameHandler.CloseCase(Int32.Parse(CaseNum.text), Description.text, Lives);
        MoneyRec.GetComponent<TMP_Text>().text = gain.ToString();
    }
}
