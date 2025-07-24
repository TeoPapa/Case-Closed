using System.Collections;
using UnityEngine;

public class TeleportCanvas : InteractableCanvas
{
    Vector2 positionToTeleport;
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
        player.ChangePlayer(GoingToInterior);


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
