using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 launchDirection = Vector2.right;
    public float launchSpeed;
    public float pullSpeed;

    Hook hook;
    GameObject player;

    void LaunchHook()
    {
        hook.launch(launchDirection, launchSpeed);
    }

    void PullPlayer()
    {

    }
    void Start()
    {
        player = transform.parent.gameObject;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        hook = GetComponentInChildren<Hook>();
        hook.Setup(player, gameObject);
        LaunchHook();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
