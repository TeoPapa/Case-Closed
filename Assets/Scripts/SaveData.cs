using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Money;

    public bool hasPlayedBefore;

    public float[] PlayerPosition;

    public int[] DestroyedObjects;

    public int[,] LevelsPlayed; 

    public SaveData() {
        Money = GameHandler.Money;

        hasPlayedBefore = GameHandler.hasPlayedBefore;

        PlayerPosition = new float[3];
        PlayerPosition[0] = GameHandler.PlayerPosition.x;
        PlayerPosition[1] = GameHandler.PlayerPosition.y;
        PlayerPosition[2] = GameHandler.PlayerPosition.z;

        DestroyedObjects = GameHandler.DestroyedStuff.ToArray();

        List<Level> Levels = GameHandler.LevelsPlayed;
        LevelsPlayed = new int[Levels.Count, 2];

        for(int i = 0; i < Levels.Count; i++) {
            LevelsPlayed[i,0] = Levels[i].getNumber();
            LevelsPlayed[i,1] = Levels[i].getMoney();
        }
    }
}
