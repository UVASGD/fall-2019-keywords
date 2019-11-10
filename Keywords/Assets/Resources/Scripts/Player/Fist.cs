using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour {

    Vector2 dir;
    float stun_duration = 5f, extend_duration = 0.1f;

    // Start is called before the first frame update
    void Awake() {
        // ignore collisions with your player
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), transform.parent.GetComponent<Collider2D>());
    }

    public void Punch(Vector2 dir) {
        if (gameObject.activeSelf)
            return;
        // fx and stuff
        gameObject.SetActive(true);
        StartCoroutine(ExtendFist(dir));
    }

    private IEnumerator ExtendFist(Vector2 dir) {
        transform.up = dir;
        yield return new WaitForSeconds(extend_duration);
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerController>().Bonk(dir, stun_duration);
        }
    }
}
