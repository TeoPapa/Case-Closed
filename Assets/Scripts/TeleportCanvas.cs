using System.Collections;
using UnityEngine;

public class TeleportCanvas : InteractableCanvas
{
    Vector2 positionToTeleport;

    public PlayerInstanceValues City;
    public PlayerInstanceValues Interior;

    bool GoingToInterior;

    Animator Fade;
    protected override void InitializeCanvas() {
        Fade = GetComponent<Animator>();
    }
    protected override void OpenCanvas() {
        StopAllCoroutines();
        StartCoroutine(TeleportPlayer());
    }

    IEnumerator TeleportPlayer() {
        
        Fade.SetBool("Fading", true);
        yield return new WaitForSeconds(1f);

        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        player.transform.position = positionToTeleport;

        if (GoingToInterior) {//Goes in interior
            player.ChangeMovementValues(Interior.Scale, Interior.Speed, Interior.Size, Interior.Bubble);
        } else {//Goes to city
            player.ChangeMovementValues(City.Scale, City.Speed, City.Size, City.Bubble);
        }


        yield return new WaitForSeconds(.1f);
        Fade.SetBool("Fading", false);
        yield return new WaitForSeconds(1f);
        Close();
    }

    public void TeleportationSet(Vector2 position, bool interior) {
        positionToTeleport = position;
        GoingToInterior = interior;
    }


}
