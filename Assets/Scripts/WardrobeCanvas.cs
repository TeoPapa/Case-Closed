using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeCanvas : InteractableCanvas
{
    public List<Sprite> Hats;
    public SpriteRenderer HatSprite;

    protected override void InitializeCanvas() {
        HatSprite.sprite = Hats[GameHandler.Hat];
    }

    public void changeHat(int Index) {
        if (Index < 0 || Index >= Hats.Count) {
            GameHandler.Hat = 0;
            HatSprite.sprite = null;

            return;
        }

        HatSprite.sprite = Hats[Index];
        GameHandler.Hat = Index;
        GameHandler.Save();
    }
}
