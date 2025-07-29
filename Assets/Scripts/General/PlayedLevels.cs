using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayedLevels : MonoBehaviour
{
    public GameObject LevelsPanel;

    public GameObject LevelsParent;

    public GameObject LevelObject;

    public GameObject ScrollRect;
    GridLayoutGroup Group;

    private void Start() {
        LevelsPanel.SetActive(false);
        Group = LevelsParent.GetComponent<GridLayoutGroup>();
    }

    public void OpenLevels() {
        LevelsPanel.SetActive(true);
        List<Level> Levels = GameHandler.LevelsPlayed;

        LevelsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(LevelsParent.GetComponent<RectTransform>().sizeDelta.x, Levels.Count * 320);

        Group.cellSize = new Vector2(LevelsParent.GetComponent<RectTransform>().rect.width, Group.cellSize.y);

        List<CaseValue> Cases = new List<CaseValue> (FindObjectsByType<CaseValue>(FindObjectsSortMode.None));
        

        foreach (Level level in Levels) {
            GameObject o = Instantiate(LevelObject, LevelsParent.transform);

            CaseValue ca = Cases.Find((CaseValue c) => { return c.Level.getNumber() == level.getNumber(); });

            o.GetComponent<LevelInformation>().SetLevel(level.getNumber(), (level.getMoney() / 5), ca);
        }

        FindFirstObjectByType<PlayerMovement>().CanMove = false;
    }

    public void CloseLevels() {
        foreach (Transform child in LevelsParent.transform)
            Destroy(child.gameObject);
        LevelsPanel.SetActive(false);

        GameHandler.Case = null;

        FindFirstObjectByType<PlayerMovement>().CanMove = true;
    }
}
