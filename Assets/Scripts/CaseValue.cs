using System.Collections.Generic;
using UnityEngine;

public class CaseValue : MonoBehaviour {
    public int Level; //Case Level Number

    public string Description; //Case's Description

    public List<CaseItemType> Location; //All The Location Items
    public List<CaseItemType> Weapons; //All The Weapon Items
    public List<CaseItemType> StolenItems; //All The Stolen Items
    public List<CaseItemType> People; //All The People Involved

    public void ClearLists() {
        Location.Clear();
        Weapons.Clear();
        StolenItems.Clear();
        People.Clear();
    }
}
