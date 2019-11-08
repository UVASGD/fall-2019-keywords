using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireable : MonoBehaviour {

    protected PlayerController player;

    public virtual void PickUp(GameObject player)
    {
        this.player = player.GetComponent<PlayerController>();
    }

    public virtual void Fire(Vector2 direction, GameObject firingPlayer) {
    }
}