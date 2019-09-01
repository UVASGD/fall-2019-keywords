using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			Debug.Log ("Player " + other.gameObject.GetComponent<PlayerInfo> ().playerNum + " Has Won The Game!");
		}
	}
}
