using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordOverlayHandler : MonoBehaviour {
	public WordOverlay wordPrefab;

	private Queue<WordOverlay> overlays;

	private void Awake () {
		overlays = new Queue<WordOverlay>();
	}

	public void CreateWord (string word) {
		GameObject instance = Instantiate(wordPrefab.gameObject, transform) as GameObject;
		WordOverlay overlay = instance.GetComponent<WordOverlay>();
		if (overlays.Count >= 10) {
			WordOverlay wordToRemove = overlays.Dequeue();
			Destroy(wordToRemove.gameObject);
		}
		overlays.Enqueue(overlay);
		overlay.InitializeWord(word);
	}

	public void AppearWords () {
		foreach (WordOverlay word in overlays) {
			if (word) {
				word.Appear();
			}
		}
	}

	public void DisappearWords () {
		foreach (WordOverlay word in overlays) {
			word.Disappear();
		}
	}
}
