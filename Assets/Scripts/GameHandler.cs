using NUnit.Framework;
using System.Collections.Generic;
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

    public static List<int> DestroyedStuff = new List<int>(); //All the destroyed objects (by ID) (Objects
                                                              //that the player has already interacted and
                                                              //can be destroyed)

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

    /* T
     */
    public static void DestroyItems() {
        List<Destroyable> b = new List<Destroyable>();

        b.AddRange(FindObjectsByType<Destroyable>(FindObjectsSortMode.None));
        foreach(Destroyable bl in b) {
            if (DestroyedStuff.Contains(bl.DestroyableID))
                bl.DestroyMe(false);
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
