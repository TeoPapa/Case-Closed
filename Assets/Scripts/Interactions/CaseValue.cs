using System;
using System.Collections.Generic;
using UnityEngine;

public class CaseValue : MonoBehaviour {
    [HideInInspector]
    public Level Level; //Case Level Number

    public int LevelNum;
    public string Description; //Case's Description

    public List<CaseItemType> CaseList;
    int Locations;
    int Weapons;
    int Items;
    int People;

    public CaseValue(Level level) {
        Level = level;
    }

    private void Start() {
        Level = new Level(LevelNum, Description);
        Locations = 0;
        Weapons = 0;
        Items = 0;
        People = 0;

        foreach(CaseItemType i in CaseList) {
            switch(i.Type) {
                case 0:
                    Locations++;
                    break;
                case 1:
                    Weapons++;
                    break;
                case 2:
                    Items++;
                    break;
                case 3:
                    People++;
                    break;
            }   
        }
    }
    public Level getLevel() { return Level; }
    public void ClearLists() {
        CaseList.Clear();
    }

    public int getCount(int x) {
        int count = 0;

        switch (x) {
            case 0:
                count = Locations;
                break;
            case 1:
                count = Weapons;
                break;
            case 2:
                count = Items;
                break;
            case 3:
                count = People;
                break;
        }

        return count;
    }

    private void OnEnable() {
        if (!(Level == null)) return;

        Level = new Level(LevelNum, Description);
        Locations = 0;
        Weapons = 0;
        Items = 0;
        People = 0;

        foreach (CaseItemType i in CaseList) {
            switch (i.Type) {
                case 0:
                    Locations++;
                    break;
                case 1:
                    Weapons++;
                    break;
                case 2:
                    Items++;
                    break;
                case 3:
                    People++;
                    break;
            }
        }
    }

    public bool Equals(CaseValue other) {
        return this.Level.getNumber() == other.Level.getNumber();
    }
}
