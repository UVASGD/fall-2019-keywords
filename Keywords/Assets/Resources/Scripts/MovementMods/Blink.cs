using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : Fireable {

    [SerializeField]
    [Range(1000f, 5000f)]
    private float DASH_SPEED = 3000f;
    private float DISTANCE = 1.2f;
    private LayerMask WALL_LAYER_MASK;

    protected override void Start() {
        base.Start();
        WALL_LAYER_MASK = LayerMask.GetMask("Wall");
    }

    public override void Fire(Vector2 v, GameObject firingPlayer) {
        if (!cooldown.Check()) {
            return;
        }
        Rigidbody2D rb = firingPlayer.GetComponent<Rigidbody2D>();
        Vector2 dest = rb.position + v * DISTANCE;
        RaycastHit2D raycast = Physics2D.CircleCast(dest, 0.05f, Vector2.up, 0.01f, WALL_LAYER_MASK);
        if (raycast.collider == null) {
            rb.position = dest;
        } else {
            // Debug.Log("Blink denied by " + raycast.collider.gameObject.name);
        }
    }
}
