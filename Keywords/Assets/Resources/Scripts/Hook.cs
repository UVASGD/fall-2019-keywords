using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    float maxLength = 3f;
    public bool hasFired;
    float curLength;
    public SpringJoint2D spring;
    public bool hasHit, hasReturned, toReturn;
    float maxReturnTime = 5f;

    public GameObject player, grapplingHook;

    Grabbable target;


     public void launch(Vector2 dir, float speed)
    {
        if (hasFired)
            return;

        print("Launching");
        transform.parent = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        hasFired = true;
        hasReturned = false;
        hasHit = false;
        rb.velocity = dir * speed;
        curLength = 0;
        spring.enabled = false;
    }

    public void launchReturn()///for when we run out of rope/time and hook is sent back to player
    {
        if (hasReturned)
        {
            StopAllCoroutines();
            return;
        }
        toReturn = true;
        print("Returning launch");
        //change physics type to dynamic
        rb.bodyType = RigidbodyType2D.Dynamic;
        //change anchor to gun
        spring.connectedBody = grapplingHook.GetComponent<Rigidbody2D>();
        //enable spring
        spring.enabled = true;
    }
    public void pullPlayer()
    {
        print("Pulling player");
        //change physics to kinematic
        rb.bodyType = RigidbodyType2D.Kinematic;
        //change anchor to player
        spring.connectedBody = player.GetComponent<Rigidbody2D>();
        //enable spring
        spring.enabled = true;
        StartCoroutine(ReturnTime());
    }

    public void grab(Grabbable target)
    {
        print("Grabbing: " + target);
        target.grab(rb);
        spring.connectedBody = grapplingHook.GetComponent<Rigidbody2D>();
        spring.enabled = true;
        this.target = target;
        launchReturn();
        StartCoroutine(ReturnTime());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("Triggered: " + collision.gameObject.name + " ree");
        if (hasFired)
        {
            if(collision.gameObject == player || collision.gameObject == grapplingHook)
            {
                if (hasHit || toReturn)
                {
                    print("Collided with home: " + collision.gameObject);
                    hasFired = false;
                    hasReturned = true;
                    rb.velocity = Vector2.zero;
                    curLength = 0;
                    spring.enabled = false;
                    toReturn = false;
                    if (target)
                        this.target.ungrab();
                    transform.position = grapplingHook.transform.position;
                    transform.parent = grapplingHook.transform;
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    // allow the gun to be fired again
                }
            }
            else if (!hasHit)
            {
                if (collision.GetComponent<Grabbable>())
                {
                    hasHit = true;
                    rb.velocity = Vector2.zero;
                    curLength = 0;
                    grab(collision.GetComponent<Grabbable>());
                }
                else if (collision.CompareTag("Wall"))
                {
                    hasHit = true;
                    rb.velocity = Vector2.zero;
                    curLength = 0;
                    pullPlayer();
                }
            }
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
        spring.enabled = false;
    }

    public void Setup(GameObject player, GameObject grapplingHook)
    {
        print("Set up");
        this.player = player;
        this.grapplingHook = grapplingHook;
        spring.connectedBody = grapplingHook.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    IEnumerator ReturnTime()
    {
        yield return new WaitForSeconds(maxReturnTime);
        print("We've reached return time");
        launchReturn();
        if (target)
            this.target.ungrab();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFired && !hasHit)
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
