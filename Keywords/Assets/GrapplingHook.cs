using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : Fireable
{
    // Start is called before the first frame update
    Vector2 launchDirection = Vector2.right;
    public float launchSpeed;
    public float pullSpeed;

    Hook hook;
    GameObject player;

    public void Fire(Vector2 direction)
    {
        print(hook == null);
        hook.launch(direction, launchSpeed);
    }   

    public void onPickup()
    {
        player = transform.parent.gameObject;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        hook = GetComponentInChildren<Hook>();
        print("before setup, " + (hook == null));
        hook.Setup(player, gameObject);
        print("after setup, " + (hook == null));

    }

    void PullPlayer()
    {

    }
    protected override void Start()
    {
        /*player = transform.parent.gameObject;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        hook = GetComponentInChildren<Hook>();
        print("before setup, " + (hook == null));
        hook.Setup(player, gameObject);
        print("after setup, " + (hook == null));*/

    }

    // Update is called once per frame
    void Update()
    {
        //print("hook is " + (hook == null));
    }
}
