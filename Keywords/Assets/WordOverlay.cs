using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordOverlay : MonoBehaviour
{
	public float timeUntilDecay = 10f;
	public float timeSpentDecaying = 2f;

	public bool coroutineInitialized;
	public bool coroutinePaused;

	public TMP_Text text;

	private Color tempColor;

    // Start is called before the first frame update
    void Start() {
		//text = GetComponent<TMP_Text>();
		tempColor = text.color;
		coroutinePaused = false;
		coroutineInitialized = false;
    }

	private void Update () {
		if (Input.GetKeyDown(KeyCode.J)) {
			Disappear();
		}
		else if (Input.GetKeyDown(KeyCode.K)) {
			Appear();
		}
	}

	public void InitializeWord(string word) {
		if (coroutineInitialized) return;
		coroutineInitialized = true;
		print("Initializing");
		text.SetText(word);
		StartCoroutine(DisappearCR());
	}

	public void Appear () {
		coroutinePaused = true;
		print("Appear");
		tempColor = text.color;
		text.color = new Color(tempColor.r, tempColor.g, tempColor.b, 1f);
	}

	public void Disappear () {
		print("Disappear");
		text.color = tempColor;
		coroutinePaused = false;
	}

	IEnumerator DisappearCR () {
		print("DISSAPPEYAH");
		//if (!coroutineInitialized) {
		print("Waiting...");
		float t = 0f;
		while (t < timeUntilDecay) {
			if (!coroutinePaused) {
				t += Time.deltaTime;
				print(t);
				yield return new WaitForEndOfFrame();
			}
		}
		t = 0f;
		while (t < timeSpentDecaying) {
			print("Going down...");
			if (!coroutinePaused) {
				text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime / timeSpentDecaying);
				t += Time.deltaTime;
				print(t);
				yield return new WaitForEndOfFrame();
			}
			}
		//}
	}
}
