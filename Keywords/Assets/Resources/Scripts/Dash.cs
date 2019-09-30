using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    [Range(10f, 100f)]
    private float dashSpeed = 10f;

    public void Fire(Vector2 v,Rigidbody2D rb)
    {

        StartCoroutine(DashCR(v,rb));
    }

    private IEnumerator DashCR(Vector2 v,Rigidbody2D rb)
    {
        float t = 0f;
        //movementEnabled = false;
        while (t < .25f)
        {
            rb.velocity = v * dashSpeed;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
       // movementEnabled = true;
    }
}
