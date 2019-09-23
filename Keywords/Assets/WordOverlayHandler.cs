using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordOverlayHandler : MonoBehaviour {
	public WordOverlay wordPrefab;

	public void CreateWord (string word) {
		GameObject instance = Instantiate(wordPrefab.gameObject, transform) as GameObject;
		WordOverlay overlay = instance.GetComponent<WordOverlay>();
		//overlay.transform.SetParent(transform);
		//overlay.transform.SetAsFirstSibling();
		overlay.InitializeWord(word);
	}
}
