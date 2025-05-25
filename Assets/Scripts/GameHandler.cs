using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


/* This is a static script that handles the Game Values and holds important data
 * throught the game's runtime.
 */
public class GameHandler : MonoBehaviour
{
    public static int MovementMode = 1; //1: Joystick, 2: Arrow Keys
    public static string PlayerName = "Markus";

    public static int Money; //The money of the player
    public static int moneyValue = 5; //The value of each life in the game

    public static bool hasPlayedBefore = false; //If the player plays for the first time

    public static Vector3 PlayerPosition = new Vector3(-241f, 34f, 0); //The position that the player loads
                                                                      //in the LevelScene (Initialized to
                                                                      //a centered place)

    private static List<int> DestroyedStuff = new List<int>(); //All the destroyed objects (by ID) (Objects
                                                              //that the player has already interacted and
                                                              //can be destroyed)
    private static List<int> EnabledStuff = new List<int>();

    public static CaseValue Case; //The current Case that is going to Load to the CaseScene

    public static List<Level> LevelsPlayed = new List<Level>(); //The Levels the player has played

    public static string DefaultScene = "MainMenu"; //The default scene (Changes to LevelScene if the player hasPlayedBefore)

    public static float MusicVolume = 1f;
    public static float SfxVolume = 1f;

    public static string Code = "Teo'sTestingEnv1ronment";

    /* The method that initializes and starts the CaseScene
     */
    public static void LoadScene()
    {
        SceneManager.LoadScene("CaseScene");
    }

    /* The method that closes the CaseScene and passes the important data to the GameHandler
     */
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

    public static void AddDestroyable(int ID, bool S) {
        if( !(DestroyedStuff.Contains(ID)) ) DestroyedStuff.Add(ID);

        if(S) Save(false);
    }

    public static void AddEnablable(int ID, bool S) {
        if (!(EnabledStuff.Contains(ID))) EnabledStuff.Add(ID);

        if(S) Save(false);
    }

    public static void EnableItems() {
        Debug.Log("Enabled!");
        List<Enablable> e = new List<Enablable>();
        e.AddRange(FindObjectsByType<Enablable>(FindObjectsSortMode.None));

        foreach (Enablable en in e) {
            if (EnabledStuff.Contains(en.EnableID))
                en.EnableMe(false);
        }
    }

    public static int[] GetEnables() {
        return EnabledStuff.ToArray();
    }

    public static int[] GetDisables() {
        return DestroyedStuff.ToArray();
    }

    public static void DestroyItems() {
        Debug.Log("Disabled!");
        List<Destroyable> des = new List<Destroyable>();

        des.AddRange(FindObjectsByType<Destroyable>(FindObjectsSortMode.None));
        foreach(Destroyable d in des) {
            if (DestroyedStuff.Contains(d.DestroyableID)) {
                d.DestroyMe(false);

                Debug.Log("Destroyed: " + d.DestroyableID);
            }
        }


        Debug.Log("Destroyed stuff contains:");
        foreach (int i in DestroyedStuff)
            Debug.Log(i);
    }



    public static bool isDestroyed(int ID) {
        return DestroyedStuff.Contains(ID);
    }

    public static bool isEnabled(int ID) {
        return EnabledStuff.Contains(ID);
    }

    public static void Save(bool FromCase) {
        Vector3 PlayerPos = new Vector3();
            
        try {
            PlayerPos = FindFirstObjectByType<PlayerMovement>().transform.position;
        }
        catch (NullReferenceException e){
            Debug.Log(e.ToString());
        }

        if(!hasPlayedBefore || FromCase) {
            PlayerPos = GameHandler.PlayerPosition;
            hasPlayedBefore = true;
        }

        PlayerPosition = PlayerPos;

        DestroyedStuff = DestroyedStuff.Distinct().ToList();
        EnabledStuff = EnabledStuff.Distinct().ToList();

        Saver.Save();
    }

    public static void Load() {
        SaveData data = Saver.Load();

        if (data == null) {
            return;
        }
        MovementMode = data.MovementMode;

        PlayerName = data.Name;

        Money = data.Money;

        hasPlayedBefore = data.hasPlayedBefore;

        PlayerPosition.x = data.PlayerPosition[0];
        PlayerPosition.y = data.PlayerPosition[1];
        PlayerPosition.z = data.PlayerPosition[2];

        DestroyedStuff.AddRange(data.DestroyedObjects);
        EnabledStuff.AddRange(data.EnabledObjects);

        LevelsPlayed.Clear();

        int[,] Lvls = data.LevelsPlayed;
        
        for (int i = 0; i < Lvls.GetLength(0); i++) {
            LevelsPlayed.Add(new Level(Lvls[i, 0], Lvls[i, 1]));
        }

        if (hasPlayedBefore) DefaultScene = "LevelScene";
    }

    public static void Clear() {
        EnabledStuff.Clear();
        DestroyedStuff.Clear();
    }
}
