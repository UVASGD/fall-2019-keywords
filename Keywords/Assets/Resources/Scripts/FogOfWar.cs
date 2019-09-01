using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {

	public List<GameObject> neighbors;//Fog of War objects adjacent to this one and in the same room

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			HideMyself ();
		}
	}

	void HideMyselfAndMyNeighbors(){
		Invoke ("HideMyself", 0.05f);
	}

	void HideMyself(){
		foreach (GameObject neighbor in neighbors) {
			if (neighbor.GetComponent<FogOfWar> ().neighbors.Contains (gameObject)) {
				neighbor.GetComponent<FogOfWar> ().neighbors.Remove (gameObject);
			}
			neighbor.GetComponent<FogOfWar> ().HideMyselfAndMyNeighbors();
		}
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		gameObject.GetComponent<BoxCollider2D>().enabled = false;//prevent repeat triggering
	}
}
