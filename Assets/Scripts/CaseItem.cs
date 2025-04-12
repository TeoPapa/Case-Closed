using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CaseItem : MonoBehaviour
{
    public Color Hint;
    public Color Culprit;

    public GameObject ThisObject;

    public GameObject CardImage;
    public Text CardName;
    public TMP_Text CardDescription;

    public Animator Anim;

    public bool isInCase;

    public void Clicked()
    {
        Color CardColor = Hint;
        if (isInCase) {
            CardColor = Culprit;
        }

        Anim.SetBool("Turned", true);

        ColorBlock cb = this.gameObject.GetComponent<UnityEngine.UI.Button>().colors;
        cb.normalColor = CardColor;
        cb.selectedColor = CardColor;
        cb.highlightedColor = CardColor;
        cb.pressedColor = CardColor;
        this.gameObject.GetComponent<UnityEngine.UI.Button>().colors = cb;

        CardDescription.gameObject.SetActive(true);

        

        FindObjectOfType<CaseHandler>().FoundItem(isInCase);
    }

    public void ObjectCreated(CaseItemType ob) {

        CardImage.GetComponent<UnityEngine.UI.Image>().sprite = ob.CardFace;
        CardName.text = ob.Name;
        CardDescription.text = ob.Description;
        isInCase = ob.IsInCase;

        CardDescription.gameObject.SetActive(false);
    }
}
