using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : Fireable
{

    [SerializeField]
    [Range(1000f, 5000f)]
    private float DASH_SPEED = 3000f;

    public override void Fire(Vector2 v, GameObject firingPlayer)
    {
        Rigidbody2D rb = firingPlayer.GetComponent<Rigidbody2D>();
        rb.AddForce(v * DASH_SPEED);
    }
}
