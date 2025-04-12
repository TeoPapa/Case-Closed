using System;
using System.Collections.Generic;
using UnityEngine;

public class CaseValue : MonoBehaviour {
    [HideInInspector]
    public Level Level; //Case Level Number

    public int LevelNum;
    public string Description; //Case's Description

    public List<CaseItemType> Location = new List<CaseItemType>(); //All The Location Items
    public List<CaseItemType> Weapons = new List<CaseItemType>(); //All The Weapon Items
    public List<CaseItemType> StolenItems = new List<CaseItemType>(); //All The Stolen Items
    public List<CaseItemType> People = new List<CaseItemType>(); //All The People Involved

    private void Start() {
        Level = new Level(LevelNum, Description);
    }
    public Level getLevel() { return Level; }
    public void ClearLists() {
        Location.Clear();
        Weapons.Clear();
        StolenItems.Clear();
        People.Clear();
    }
}
