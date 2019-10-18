using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initial, dir;
    float launchSpeed, pullSpeed;
    public float stopDistance;

    float curLength = 0f;
    float maxLength = 3f;
    bool hasFired, hasHit;

    public GameObject player, grapplingHook;
    Rigidbody2D rbPlayer;
    Transform tPlayer;

     public void launch(Vector3 dir, float launchSpeed, float pullSpeed)
    {
        if (hasFired) //Cancel the Grappling Hook
        {
            Stop();
            return;
        }

        Game.EnablePhysics(gameObject);
        transform.parent = null; //Separate Hook from GrapplingHook Body
        hasFired = true;
        hasHit = false;
        this.dir = dir;
        this.launchSpeed = launchSpeed;
        this.pullSpeed = pullSpeed;
        curLength = 0;
    }

    public void Stop()//for when we run out of rope/time
    {
        transform.parent = grapplingHook.transform;
        transform.localPosition = initial;
        transform.eulerAngles = Vector3.zero;
        hasFired = false;
        hasHit = false;
        Game.DisablePhysics(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasFired && !hasHit)
        {
            if (collision.CompareTag("Wall"))
            {
                curLength = 0;
                hasHit = true;
            }
        }
    }

    public void Setup(GameObject player, GameObject grapplingHook)
    {
        this.player = player;
        this.grapplingHook = grapplingHook;
        rbPlayer = player.GetComponent<Rigidbody2D>();
        tPlayer = player.GetComponent<Transform>();
        initial = transform.localPosition;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (hasFired && !hasHit)
        {
            if (curLength < maxLength)
            {
                transform.position = transform.position + (Time.deltaTime * dir * launchSpeed);
                curLength += Time.deltaTime;
            }
            else
            {
                Stop();
            }
        }
    }

    void LateUpdate()
    {
        if (hasHit)
        {
            Vector3 dif = transform.position - tPlayer.position;
            if (curLength < maxLength && dif.magnitude > stopDistance)
            {
                dir = dif.normalized * pullSpeed;
                rbPlayer.velocity += (Vector2)dir;
                curLength += Time.deltaTime;
            }
            else
            {
                Stop();
            }
        }
    }
}
