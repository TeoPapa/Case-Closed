using UnityEngine;

public class WalkingTeleportation : MonoBehaviour
{
    public Vector2 TeleportPosition;
    public bool isInterior;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.gameObject.tag.Equals("Player") ) return;

        TeleportCanvas tp = FindFirstObjectByType<TeleportCanvas>();

        tp.TeleportationSet(TeleportPosition,isInterior);
        tp.Open();
    }
}
