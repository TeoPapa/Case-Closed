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
    public int MyID;
    public List<Destroyable> dest;

    private void Awake() {
        this.gameObject.AddComponent<Destroyable>();
        dest.Add(this.GetComponent<Destroyable>());
        dest[dest.Count-1].DestroyableID = MyID;
    }

    protected override string setBubble() {
        if(Name.Equals("")) return "Something Blocks My Path!";

        return Name;
    }

    protected override InteractableCanvas setCanvas() {
        return FindFirstObjectByType<BlockCanvas>();
    }
}
