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
    public List<GameObject> TypesButton; //0: Location, 1: Weapons, 2: Items, 3: People

    [Header("The Panels Of Each Button")]
    public List<GameObject> Panels; //0: Location, 1: Weapons, 2: Items, 3: People

    [Header("The Parent Of Each Page")]
    public List<GameObject> Parents; //0: Location, 1: Weapons, 2: Items, 3: People

    public GameObject Scroll;

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

    public GameObject OpenPanel;
    public GameObject OpenButton;

    public CaseValue CurrentCase;

    public void Start() {
        List<GridLayoutGroup> Groups = new List<GridLayoutGroup>();


        foreach(GameObject o in Parents) {
            Groups.Add(o.GetComponent<GridLayoutGroup>());
        }

        CurrentCase = GameHandler.Case;

        OpenPanel.SetActive(true);
        OpenButton.SetActive(true);

        EndScreen.SetActive(false);
        WinScreen.SetActive(false);

        currFoundtxt.GetComponent<TMP_Text>().text = "0";
        cuurentItems.GetComponent<TMP_Text>().text = "0";

        foreach(GameObject o in TypesButton)
            o.gameObject.SetActive(true);

        foreach(GameObject o in Panels)
            o.gameObject.SetActive(true);


        CaseNum.text = CurrentCase.Level.getNumber().ToString();
        Description.text = CurrentCase.Description;

        Life1.SetActive(true);
        Life2.SetActive(true);
        Life3.SetActive(true);

        foreach (CaseItemType i in CurrentCase.CaseList) {
            Everything++;
            if (i.isInCase())
                AllTheObjects++;
            GameObject o = Instantiate(CaseItemPrefab, (RectTransform)Parents[i.Type].transform);
            o.GetComponent<CaseItem>().ObjectCreated(i);
        }

       allobjtxt.GetComponent<TMP_Text>().text = AllTheObjects.ToString();
       AllItems.GetComponent<TMP_Text>().text = Everything.ToString();

        ChangeColors(Mode);

        for (int i = 0; i < TypesButton.Count; i++) {
            if(CurrentCase.getCount(i) <= 0) TypesButton[i].gameObject.SetActive(false);

            Panels[i].gameObject.SetActive(false);
        }
    }

    public void Open() {
        OpenButton.SetActive(false);
        StartCoroutine(Openner());
    }
    IEnumerator Openner() {

        OpenPanel.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(.5f);

        OpenPanel.SetActive(false);
    }
    public void switchMode(bool mode) {
        FindFirstObjectByType<CaseAudioManager>().ChangeMode();
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
        FindFirstObjectByType<CaseAudioManager>().Win(false);
    }

    public void FoundItem(bool b) {

        FoundItems += 1;
        if (b) FoundObjectives += 1;

        currFoundtxt.GetComponent<TMP_Text>().text = FoundObjectives.ToString();
        cuurentItems.GetComponent<TMP_Text>().text = FoundItems.ToString();


        if (b != Mode) {
            loseLife();
        }

        if (FoundItems == Everything) {
            StopAllCoroutines();
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter() {
        yield return new WaitForSeconds(.5f);


        GameWon();
    }

    void GameWon() {
        FindFirstObjectByType<CaseAudioManager>().Win(true);
        WinScreen.SetActive(true);

        for (int i = 0; i < Lives; i++) {
            GameObject o = Instantiate(HintsPrefab, HintsParent.transform);
        }

        int gain = GameHandler.CloseCase(CurrentCase.Level, Lives);
        MoneyRec.GetComponent<TMP_Text>().text = gain.ToString();
        GameHandler.hasPlayedBefore = true;
        Saver.Save("LevelScene");

    }
}
