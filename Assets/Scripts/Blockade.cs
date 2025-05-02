using UnityEngine;
using UnityEngine.UIElements;

public class Blockade : Interaction
{
    public int BlockadeCost;
    public string Description;
    public int BlockadeID;

    BlockCanvas BlockadePanel;
    public override void PlayerInteraction() {
        BlockadePanel.Open(this, BlockadeCost, Description);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BlockadePanel = FindObjectOfType<BlockCanvas>();
        bubbleString = "Something Blocks My Path!";
    }
}
