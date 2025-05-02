using TMPro;
using UnityEngine;

public class BlockCanvas : MonoBehaviour
{
    public GameObject BlockPanel;
    public TMP_Text DescriptionText;
    public TMP_Text CostText;

    public GameObject YouSurePanel;
    public GameObject YouCantPanel;

    int Cost;
    string Description;
    Blockade Destroyable;

    void Start()
    {
        BlockPanel.SetActive(false);
        YouCantPanel.SetActive(false);
        YouSurePanel.SetActive(false);
    }

    public void Open(Blockade destr, int cost, string desc) {
        Destroyable = destr;

        BlockPanel.SetActive(true);
        Cost = cost;
        Description = desc;

        DescriptionText.text = Description;
        CostText.text = Cost.ToString();
    }

    public void Destroy() {
        if(GameHandler.Money < Cost) {
            YouCantPanel.SetActive(true);
            return;
        }

        YouSurePanel.SetActive(true);
    }

    public void Accept() {
        GameHandler.Money -= Cost;
        GameHandler.DestroyedStuff.Add(Destroyable.BlockadeID);
        Destroy(Destroyable.gameObject);
        Saver.Save();
    }
}
