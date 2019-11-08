using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireable : MonoBehaviour {
    public Cooldown cooldown;
    public float cooldownTime = 0f;

    protected virtual void Start() {
        cooldown = new Cooldown(cooldownTime);
    }
    protected virtual bool CheckCooldown() {
        if (cooldown.Ready()) {
            print("cooldown complete");
            cooldown.Reset();
            return true;
        }
        print("cooldown not complete");
        return false;
    }
    public virtual void Fire(Vector2 direction, GameObject firingPlayer) {
    }
}

