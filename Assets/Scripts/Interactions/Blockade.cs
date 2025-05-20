using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Blockade : Interaction
{
    public string Name = "";
    public int BlockadeCost;
    public string Description;
    public int ID = -3;
    public List<Destroyable> dest;

    void Awake() {
        gameObject.AddComponent<Destroyable>();
        dest.Add(this.GetComponent<Destroyable>());
        dest[dest.Count - 1].DestroyableID = ID;
    }

    protected override string setBubble() {
        if(Name.Equals("")) return "Something Blocks My Path!";

        return Name;
    }

    protected override InteractableCanvas setCanvas() {
        return FindFirstObjectByType<BlockCanvas>();
    }
}
