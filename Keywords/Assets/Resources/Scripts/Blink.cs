using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField]
    [Range(1000f, 5000f)]
    private float DASH_SPEED = 3000f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire(Vector2 v, Rigidbody2D rb)
    {
        rb.AddForce(v * DASH_SPEED);
    }
}
