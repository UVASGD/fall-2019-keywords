using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    float timeCounter = 0;
    // Start is called before the first frame update
    void Start()
    {

        if(gameObject.name == "Star1")
        {
            timeCounter = 160;
        }
        if (gameObject.name == "Star2")
        {
            timeCounter = 250;
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Circle(Vector2 pos)
    {
            timeCounter += .3f;
            float x = Mathf.Cos(timeCounter) * .1f;
            float y = Mathf.Sin(timeCounter) * .1f;
            transform.position = new Vector2(x + pos.x, y + pos.y + .12f);
    }

}
