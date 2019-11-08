using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown {
    float cooldown;
    float timeStarted;

    public Cooldown(float myCooldown) {
        cooldown = myCooldown;
        timeStarted = -cooldown - 1f;
    }

    public bool Ready() {
        return Time.time - timeStarted >= cooldown;
    }

    public void Reset() {
        timeStarted = Time.time;
    }

    public void SetCooldown(float newCooldown) {
        cooldown = newCooldown;
    }
}
