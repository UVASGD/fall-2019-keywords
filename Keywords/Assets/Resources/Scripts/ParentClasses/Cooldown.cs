using UnityEngine;

public class Cooldown {
    float cooldown;
    float timeStarted;
    AudioSource notReadySFX;

    public Cooldown(float cooldownTime, AudioSource notReady = null) {
        cooldown = cooldownTime;
        timeStarted = -cooldown - 1f;
        notReadySFX = GameManager.instance.sfx["CooldownNotReadySFX"];
        if (notReady) {
            notReadySFX = notReady;
        }
    }

    public bool Ready() {
        return Time.time - timeStarted >= cooldown;
    }

    public void Start() {
        timeStarted = Time.time;
    }

    public void Reset() {
        timeStarted = -cooldown - 1f;
    }

    public void SetCooldown(float cooldownTime) {
        cooldown = cooldownTime;
    }

    public bool Check() {
        if (Ready()) {
            Start();
            return true;
        }
        if (notReadySFX)
            notReadySFX.Play();
        return false;
    }
}
