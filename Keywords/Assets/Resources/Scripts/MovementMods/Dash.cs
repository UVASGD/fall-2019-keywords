using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Fireable {
    [SerializeField]
    [Range(10f, 100f)]
    private float dashSpeed = 10f, stun_duration = 2f, dashTime = 1f;
    bool dashing;


    public override void PickUp(GameObject player) {
        base.PickUp(player);
        this.player_controller.CollisionEvent += HitPlayer;
    }

    public override void Drop() {
        base.Drop();
        this.player_controller.CollisionEvent -= HitPlayer;
    }

    public override void Fire(Vector2 v, GameObject firingPlayer) {
        if (dashing)
            return;
        StartCoroutine(DashCR(v, firingPlayer.GetComponent<Rigidbody2D>()));
    }

    private IEnumerator DashCR(Vector2 v, Rigidbody2D rb) {
        float t = 0f;
        dashing = true;
        while (t < .25f) {
            rb.velocity = v * dashSpeed;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(dashTime);
        dashing = false;
    }

    private void HitPlayer(Collision2D collision) {
        if (dashing) {
            if (collision.collider.CompareTag("Player")) {
                collision.collider.GetComponent<PlayerController>().Bonk((collision.collider.transform.position - player_controller.transform.position).normalized, stun_duration);
            }
        }
    }
}
