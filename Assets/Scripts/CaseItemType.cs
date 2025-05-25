using System;
using UnityEngine;

[Serializable]
public class CaseItemType{
    public Sprite CardFace;
    public string Name;

    [TextArea(3, 1)]
    public string Description;
    public bool IsInCase;

    public int Type; //0: Location, 1: Weapons, 2: Items, 3: People

    public CaseItemType(Sprite c, string n, string d, bool ii) {
        CardFace = c;
        Name = n;
        Description = d;
        IsInCase = ii;
    }

    public Sprite getFace() {
        return CardFace;
    }

    public string getName() {
        return Name;
    }

    public string getDescription() {
        if(Description.Length <= 140) return Description;

        return Description.Substring(0, 140);
    }

    public bool isInCase() {
        return IsInCase;
    }
}