using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float maxLength;
    bool hasFired;
    float curLength;
    public SpringJoint2D spring;
    bool hasLanded;

    GameObject player, grapplingHook;


     public void launch(Vector2 dir, float speed)
    {

        if (hasFired)
            return;
        hasFired = true;
        rb.velocity = dir * speed;
        curLength = 0;
        spring.enabled = false;
    }

    public void launchReturn()///for when we run out of rope/time and hook is sent back to player
    {
        //change physics type to dynamic
        rb.bodyType = RigidbodyType2D.Dynamic;
        //change anchor to gun
        spring.connectedBody = grapplingHook.GetComponent<Rigidbody2D>();
        //enable spring
        spring.enabled = true;
    }
    public void pullPlayer()
    {
        //change physics to kinematic
        rb.bodyType = RigidbodyType2D.Kinematic;
        //change anchor to player
        spring.connectedBody = player.GetComponent<Rigidbody2D>();
        //enable spring
        spring.enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasFired)
        {
            if (hasLanded)
            {
                if(collision.gameObject == player || collision.gameObject == grapplingHook)
                {
                    hasFired = false;
                    rb.velocity = Vector2.zero;
                    curLength = 0;
                    spring.enabled = false;
                    // allow the gun to be fired again
                }
            }
            else
            {
                //GetComponent<Grabbable>, pullPlayer
                hasLanded = true;
                rb.velocity = Vector2.zero;
                curLength = 0;
                spring.enabled = false;
            }
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
    }

    public void Setup(GameObject player, GameObject grapplingHook)
    {
        this.player = player;
        this.grapplingHook = grapplingHook;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFired && !hasLanded)
        {
            if (curLength < maxLength)
            {
                curLength += Time.deltaTime;
                
            }
            else
            {
                launchReturn();
            }
        }
    }
}
