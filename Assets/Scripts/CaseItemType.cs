using System;
using UnityEngine;

[Serializable]
public class CaseItemType{
    public Sprite CardFace;
    public string Name;
    public string Description;
    public bool IsInCase;

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
        return Description;
    }

    public bool isInCase() {
        return IsInCase;
    }
}