using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : Fireable {
    // Start is called before the first frame update
    Vector2 launchDirection = Vector2.right;
    public float launchSpeed, pullSpeed;

    Hook hook;
    GameObject player;

    public override void Fire(Vector2 direction, GameObject firingPlayer) {
        hook.launch(direction, launchSpeed, pullSpeed);
    }

    public void onPickup() {
        player = transform.parent.gameObject;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        hook = GetComponentInChildren<Hook>();
        hook.Setup(player, gameObject);
    }
}
