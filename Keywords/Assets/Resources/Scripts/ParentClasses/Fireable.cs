using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireable : MonoBehaviour {
    public Cooldown cooldown;
    public float cooldownTime = 0f;

    protected virtual void Start() {
        cooldown = new Cooldown(cooldownTime);
    }
    public virtual void Fire(Vector2 direction, GameObject firingPlayer) {
    }
}

