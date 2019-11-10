using UnityEngine;

public class Cooldown {
    float cooldown;
    float timeStarted;

    public Cooldown(float cooldownTime) {
        cooldown = cooldownTime;
        timeStarted = -cooldown - 1f;
    }

    public bool Ready() {
        return Time.time - timeStarted >= cooldown;
    }

    public void Reset() {
        timeStarted = Time.time;
    }

    public void SetCooldown(float cooldownTime) {
        cooldown = cooldownTime;
    }

    public bool Check() {
        if (Ready()) {
            Reset();
            return true;
        }
        return false;
    }
}
