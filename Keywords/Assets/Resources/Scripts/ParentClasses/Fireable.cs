using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireable : MonoBehaviour {

    protected PlayerController player_controller;

    public virtual void PickUp(GameObject player) {
        this.player_controller = player.GetComponent<PlayerController>();
    }

    public virtual void Fire(Vector2 direction, GameObject firingPlayer) {
    }

    public virtual void Drop() {

    }
}