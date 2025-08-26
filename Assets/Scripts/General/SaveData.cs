using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* This is a class that converts the important data of the game into saveable data
 */

[System.Serializable]
public class SaveData
{

    public float VersionNumber; //The Current Version Of The Game

    public int MovementMode; //The movement scheme the player prefers

    public string Name; //The name of the player

    public bool PlayerInInterior = true; //The boolean that says if the player is inside or outside

    public int Money; //The money of the player

    public float[] CurrentTrack;

    public bool hasPlayedBefore; //If it's the first time the player plays the game



    public float[] PlayerPosition; //Where is the last position the player stood

    public int[] DestroyedObjects; //Which objects the player has "destroyed"
                                   //(interacted and remove from the game)

    public int[] EnabledObjects;

    public int[,] LevelsPlayed; //Which levels the player played
    
    public int Hat;


    /* On creation, the constructor of SaveData takes the data from the GameHandler
     * and converts it to basic saving types (int, float, bool etc).
     */
    public SaveData() {
        VersionNumber = GameHandler.GameVersion;

        Hat = GameHandler.Hat;

        PlayerInInterior = GameHandler.IsInside;
        MovementMode = GameHandler.MovementMode;
        Name = GameHandler.PlayerName;//Name and Money
        Money = GameHandler.Money;    //need no conversion

        Vector3 pos = new Vector3(0,0,0);

        if(GameHandler.CurrentTrack != null)
          pos = GameHandler.CurrentTrack.position;

        CurrentTrack = new float[3];
        CurrentTrack[0] = pos.x;
        CurrentTrack[1] = pos.y;
        CurrentTrack[2] = pos.z;

        hasPlayedBefore = GameHandler.hasPlayedBefore; //Same goes for hasPlayedBefore

        PlayerPosition = new float[3]; //This is the Vector3 position of the player that takes:
        PlayerPosition[0] = GameHandler.PlayerPosition.x;// 1) The X position of the platyer
        PlayerPosition[1] = GameHandler.PlayerPosition.y;// 2) The Y position of the player
        PlayerPosition[2] = GameHandler.PlayerPosition.z;// 3) The Z position of the player

        DestroyedObjects = GameHandler.GetDisables(); //Passes all the destroyed ID
                                                                 //of the objects to an array

        EnabledObjects = GameHandler.GetEnables(); //Passes all the Enabled IDs of the
                                                             //objects into an array

        List<Level> Levels = GameHandler.LevelsPlayed; //Creates a List with all the levels
                                                       //played and then converts it to an
        LevelsPlayed = new int[Levels.Count, 2];       //[X, 2], where X how many levels 
                                                       //have been played and 2: 0: Level Number,
        for (int i = 0; i < Levels.Count; i++) {       //1: Money From The Level
            LevelsPlayed[i,0] = Levels[i].getNumber();
            LevelsPlayed[i,1] = Levels[i].getMoney();
        }
    }
}
