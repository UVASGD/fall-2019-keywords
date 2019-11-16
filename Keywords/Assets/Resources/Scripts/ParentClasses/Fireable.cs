using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireable : MonoBehaviour {
    public Cooldown cooldown;
    public float cooldownTime = 0f;

    protected virtual void Start() {
        cooldown = new Cooldown(cooldownTime);
    }

    protected PlayerController player_controller;

    public virtual void PickUp(GameObject player) {
        this.player_controller = player.GetComponent<PlayerController>();
    }

    public virtual void Fire(Vector2 direction, GameObject firingPlayer) {
    }

    public virtual void Drop() {

    }
}