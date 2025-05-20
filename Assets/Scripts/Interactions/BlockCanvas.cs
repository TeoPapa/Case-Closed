using NUnit.Framework.Internal;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BlockCanvas : InteractableCanvas
{
    public TMP_Text DescriptionText;
    public TMP_Text CostText;

    public GameObject YouSurePanel;
    public GameObject YouCantPanel;

    protected override void InitializeCanvas() {
        YouCantPanel.SetActive(false);
        YouSurePanel.SetActive(false);
    }

    protected override void CloseCanvas() {
        YouSurePanel.SetActive(false);
    }

    protected override void OpenCanvas() {
        Blockade des = (Blockade)Inter;

        DescriptionText.text = des.Description;
        CostText.text = des.BlockadeCost.ToString();
    }

    public void Destroy() {
        Blockade des = (Blockade)Inter;

        if (GameHandler.Money < des.BlockadeCost) {
            YouCantPanel.SetActive(true);
            return;
        }

        YouSurePanel.SetActive(true);
    }

    public void Accept() {
        Blockade des = (Blockade)Inter;

        GameHandler.Money -= des.BlockadeCost;
        foreach(Destroyable d in des.dest)
            d.DestroyMe(true);

        GameHandler.Save(false);
        Close();
    }
}
