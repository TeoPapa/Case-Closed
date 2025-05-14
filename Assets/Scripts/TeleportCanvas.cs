using System.Collections;
using UnityEngine;

public class TeleportCanvas : InteractableCanvas
{
    Vector2 positionToTeleport;

    float PlayerScaleOnCity = 3f;
    float CameraSizeOnCity = 7f;
    Vector3 BubbleOnCity = new Vector3(143f, 376.5f, 0f);

    float PlayerScaleOnInterior = 8f;
    float CameraSizeOnInterior = 12f;
    Vector3 BubbleOnInterior = new Vector3(164f, 517f, 0f);

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
        RectTransform r = (RectTransform)player.BubbleCanvas.transform;
        if (GoingToInterior) {
            player.transform.localScale = new Vector3(PlayerScaleOnInterior, PlayerScaleOnInterior, 1);
            player.GetComponentInChildren<Camera>().orthographicSize = CameraSizeOnInterior;
            r.position = BubbleOnInterior;
        } else {
            player.transform.localScale = new Vector3(PlayerScaleOnCity, PlayerScaleOnCity, 1);
            player.GetComponentInChildren<Camera>().orthographicSize = CameraSizeOnCity;
            r.position = BubbleOnCity;
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
