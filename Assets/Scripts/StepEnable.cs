using UnityEngine;

public class StepEnable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.tag.Equals("Player")) return;

        GetComponent<Enablable>().EnableMe(true);
        GetComponent<Destroyable>().DestroyMe(true);
    }
}
