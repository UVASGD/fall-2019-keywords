using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour {

    Vector2 dir;
    float stun_duration = 5f, extend_duration = 0.12f;
    private Cooldown punchCooldown;
    public float missCooldownTime = 1f, hitCooldownTime = 7f;
    AudioSource whiffSFX;

    // Start is called before the first frame update
    void Awake() {
        // ignore collisions with your player
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), transform.parent.GetComponent<Collider2D>());
        whiffSFX = GameManager.instance.sfx["WhiffSFX"];
    }

    public void Punch(Vector2 dir) {
        if (punchCooldown == null)
            punchCooldown = new Cooldown(missCooldownTime);
        if (!punchCooldown.Check())
            return;
        if (gameObject.activeSelf)
            return;
        // fx and stuff
        gameObject.SetActive(true);
        punchCooldown.SetCooldown(missCooldownTime);
        StartCoroutine(ExtendFist(dir));
    }

    private IEnumerator ExtendFist(Vector2 dir) {
        transform.up = dir;
        this.dir = dir;
        whiffSFX.Play();
        yield return new WaitForSeconds(extend_duration);
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            punchCooldown.SetCooldown(hitCooldownTime);
            collision.GetComponent<PlayerController>().Bonk(dir, stun_duration);
        }
    }
}
