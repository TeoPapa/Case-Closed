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

    public static List<int> DestroyedStuff = new List<int>();

    public static CaseValue Case = new CaseValue();

    public static List<Level> LevelsPlayed = new List<Level>();

    public static string DefaultScene = "MainMenu";

    public static void LoadScene(CaseValue ca)
    {
        Case.ClearLists();
        
        Case = ca;

        SceneManager.LoadScene("CaseScene");
    }

    public static int CloseCase(Level lv, int hnt) {
        int mon = hnt*moneyValue;
        int moneyWon = 0;
        

        Level indx = LevelsPlayed.Find( (Level l) =>  l.Equals(lv) );
        if(indx == null ) {
            LevelsPlayed.Add(lv);
            indx = lv;
        }

        moneyWon = mon - indx.getMoney();
        if (moneyWon < 0) moneyWon = 0;
        else indx.setMoney(mon);

        Money += moneyWon;

        return moneyWon;
    }

    public static void DestroyItems() {
        List<Blockade> b = new List<Blockade>();

        b.AddRange(FindObjectsOfType<Blockade>());
        foreach(Blockade bl in b) {
            if (DestroyedStuff.Contains(bl.BlockadeID))
                Destroy(bl.gameObject);
        }
    }

    public static void Load() {
        SaveData data = Saver.Load();

        if (data == null) {
            return;
        }

        Money = data.Money;

        hasPlayedBefore = data.hasPlayedBefore;

        PlayerPosition.x = data.PlayerPosition[0];
        PlayerPosition.y = data.PlayerPosition[1];
        PlayerPosition.z = data.PlayerPosition[2];

        DestroyedStuff.AddRange(data.DestroyedObjects);

        int[,] Lvls = data.LevelsPlayed;

        for(int i = 0; i < Lvls.GetLength(0); i++) {
            LevelsPlayed.Add(new Level(Lvls[i,0], Lvls[i,1]));
        }

        if (hasPlayedBefore) DefaultScene = "LevelScene";
    }
}
