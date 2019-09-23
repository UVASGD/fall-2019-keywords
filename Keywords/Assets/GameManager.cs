using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public static Words words;
	public static MakeWalls makeWalls;
	public static Quit quit;

	private void Awake () {
		if (instance) {
			Destroy(gameObject);
		} else {
			instance = this;
		}

		words = GetComponent<Words>();
		makeWalls = GetComponent<MakeWalls>();
		quit = GetComponent<Quit>();
	}
}