using System;
using UnityEngine;

public class ObjectiveTrack : MonoBehaviour
{
    private Transform TargetObjective = null;
    private bool Active = false;

    public GameObject Arrow;
    public float HideDistance = 2f;


    private void Start() {
        TargetObjective = GameHandler.CurrentTrack;
    }

    void Update() {

        if (!Active || TargetObjective == null) {
            Arrow.SetActive(false);
            return;
        } else if ( Math.Abs(transform.position.x - TargetObjective.position.x) < HideDistance  && Math.Abs(transform.position.y - TargetObjective.position.y) < HideDistance) {
            Debug.Log("Hit");
            setTarget();
            return;
        }

        Arrow.SetActive(true);

        var dir = TargetObjective.position - transform.position;

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Activate(bool b) {
        Active = b;
    }

    public void setTarget(Transform t) {
        TargetObjective = t;
        GameHandler.CurrentTrack = t;
    }

    public void setTarget() {
        TargetObjective = null;
        GameHandler.CurrentTrack = null;
        Arrow.SetActive(false);
    }
}
