using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Fireable {
    [SerializeField]
    [Range(10f, 100f)]
    private float dashSpeed = 10f;

    public override void Fire(Vector2 v, GameObject firingPlayer) {
        StartCoroutine(DashCR(v, firingPlayer.GetComponent<Rigidbody2D>()));
    }

    private IEnumerator DashCR(Vector2 v, Rigidbody2D rb) {
        float t = 0f;
        while (t < .25f) {
            rb.velocity = v * dashSpeed;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
