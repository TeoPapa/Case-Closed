using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static int Money;
    public static int moneyValue = 5;

    public static bool hasPlayedBefore = false;

    public static Vector3 PlayerPosition = new Vector3(-14f, -9f, 0);

    public static CaseValue Case = new CaseValue();

    public static List<Level> LevelsPlayed = new List<Level>();

    public static string DefaultScene = "LevelScene";

    public static void LoadScene(CaseValue ca)
    {
        Case.ClearLists();

        LevelsPlayed.Add(new Level(ca.Level.getNumber(), ca.Level.getDescription()));
        Case = ca;

        SceneManager.LoadScene("CaseScene");
    }

    public static int CloseCase(int lvl, string ds, int hnt) {
        int mon = 0;

        int indx = LevelsPlayed.FindIndex((l)=> l.getDescription().Equals(ds));

        mon = hnt - LevelsPlayed[indx].getMoney();

        if(mon > 0) LevelsPlayed[indx].setMoney(hnt);

        Money += mon*moneyValue;

        if(mon < 0) mon = 0;

        hasPlayedBefore = true;

        return mon*moneyValue;
    }
}
