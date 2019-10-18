using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {

    public List<GameObject> neighbors;//Fog of War objects adjacent to this one and in the same room

    [HideInInspector]
    public Color floorColor;
    public float floorTintAlpha;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            floorColor = Game.RandomDarkColor();
            floorColor.a = floorTintAlpha;
            if (Time.time <= 0.01f) { //starting room
                floorColor.a = 0f;
            }
            HideMyself();
        }
    }

    void HideMyselfAndMyNeighbors() {
        Invoke("HideMyself", 0.05f);
    }

    void HideMyself() {
        foreach (GameObject neighbor in neighbors) {
            if (neighbor.GetComponent<FogOfWar>().neighbors.Contains(gameObject)) {
                neighbor.GetComponent<FogOfWar>().neighbors.Remove(gameObject);
            }
            neighbor.GetComponent<FogOfWar>().floorColor = floorColor;
            neighbor.GetComponent<FogOfWar>().HideMyselfAndMyNeighbors();
        }
        Game.RepositionHeight(gameObject, Height.Background);
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().color = floorColor;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;//prevent repeat triggering
    }
}
