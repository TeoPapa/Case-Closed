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
    public static string GameVersion = Application.version;


    public static Transform CurrentTrack = null;

    public static int Hat = 0; //The index of the hat that the player has selected (0 is no hat)

    public static bool IsInside = true; //Knows if the player is in an interior or an exterior place
    public static int MovementMode = 1; //1: Joystick, 2: Arrow Keys
    public static string PlayerName = "Markus";

    public static int Money; //The money of the player
    public static int moneyValue = 5; //The value of each life in the game

    public static Vector3 PlayerPosition = new Vector3(-270.5f, -4.4f, 0); //The position that the player loads
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

    /* The method that initializes and starts the CaseScene
     */
    public static void LoadScene()
    {
        PlayerPosition = FindFirstObjectByType<PlayerMovement>().gameObject.transform.position;
        Save();
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

        if(S) Save();
    }

    public static void AddEnablable(int ID, bool S) {
        if (!(EnabledStuff.Contains(ID))) EnabledStuff.Add(ID);

        if(S) Save();
    }

    public static void EnableItems() {
        List<Enablable> e = new List<Enablable>();
        e.AddRange(FindObjectsByType<Enablable>(FindObjectsSortMode.None));

        foreach (Enablable en in e) {
            if (EnabledStuff.Contains(en.EnableID)) {
                en.EnableMe(false);
            }
        }
    }

    public static int[] GetEnables() {
        return EnabledStuff.ToArray();
    }

    public static int[] GetDisables() {
        return DestroyedStuff.ToArray();
    }

    public static void DestroyItems() {
        List<Destroyable> des = new List<Destroyable>();

        des.AddRange(FindObjectsByType<Destroyable>(FindObjectsSortMode.None));
        foreach(Destroyable d in des) {
            if (DestroyedStuff.Contains(d.DestroyableID)) {
                d.DestroyMe(false);
            }
        }
    }



    public static bool isDestroyed(int ID) {
        return DestroyedStuff.Contains(ID);
    }

    public static bool isEnabled(int ID) {
        return EnabledStuff.Contains(ID);
    }

    public static void Save() {
        Vector3 PlayerPos = new Vector3();
            
        try {
            PlayerPos = FindFirstObjectByType<PlayerMovement>().transform.position;
        }
        catch (NullReferenceException){
            PlayerPos = GameHandler.PlayerPosition;
        }

        PlayerPosition = PlayerPos;

        DestroyedStuff = DestroyedStuff.Distinct().ToList();
        EnabledStuff = EnabledStuff.Distinct().ToList();

        Saver.Save();
    }

    public static void Load() {
        SaveData data = Saver.Load();

        if (data == null || !data.Version.Equals(Application.version)) {
            Debug.Log("Hit");
            return;
        }

        IsInside = data.PlayerInInterior;
        MovementMode = data.MovementMode;

        PlayerName = data.Name;

        Money = data.Money;

        PlayerPosition.x = data.PlayerPosition[0];
        PlayerPosition.y = data.PlayerPosition[1];
        PlayerPosition.z = data.PlayerPosition[2];

        Hat = data.Hat;


        Vector3 pos = new Vector3(data.CurrentTrack[0], data.CurrentTrack[1], data.CurrentTrack[2]);
        if (pos != Vector3.zero) {
            GameObject temp = new GameObject("Temporary");
            CurrentTrack = temp.transform;
            CurrentTrack.position = pos;

            Destroy(temp);
        } else {
            CurrentTrack = null;
        }

        DestroyedStuff.AddRange(data.DestroyedObjects);

        EnabledStuff.AddRange(data.EnabledObjects);

        LevelsPlayed.Clear();

        int[,] Lvls = data.LevelsPlayed;

        for (int i = 0; i < Lvls.GetLength(0); i++) {
            LevelsPlayed.Add(new Level(Lvls[i, 0], Lvls[i, 1]));
        }

        try {
            FindFirstObjectByType<PlayerMovement>().transform.position = PlayerPosition;
            FindFirstObjectByType<WardrobeCanvas>().changeHat(Hat);
        } catch (NullReferenceException e) {
            Debug.Log(e.ToString());
        }

    }

    public static bool hasPlayedLevel(Level l) {
        return LevelsPlayed.Contains(l);
    }

    public static void Clear() {
        EnabledStuff.Clear();
        DestroyedStuff.Clear();
    }
}
