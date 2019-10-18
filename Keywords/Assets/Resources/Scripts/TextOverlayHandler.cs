using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOverlayHandler : MonoBehaviour {
    public TextOverlay wordPrefab;

    public float wordFontSize;
    public float definitionFontSize;
    public int maxWords;

    private Queue<TextOverlay> wordOverlays;
    private Queue<TextOverlay> defOverlays;

    private void Awake() {
        wordOverlays = new Queue<TextOverlay>();
        defOverlays = new Queue<TextOverlay>();
    }

    public void CreateWord(string word) {
        GameObject instance = Instantiate(wordPrefab.gameObject, transform) as GameObject;
        TextOverlay overlay = instance.GetComponent<TextOverlay>();
        if (wordOverlays.Count >= maxWords) {
            TextOverlay wordToRemove = wordOverlays.Dequeue();
            Destroy(wordToRemove.gameObject);
        }
        wordOverlays.Enqueue(overlay);
        overlay.InitializeText(word.ToUpper(), wordFontSize);
        CreateDefinition(GameManager.words.GetDefinition(word));
    }

    public void CreateDefinition(string def) {
        GameObject instance = Instantiate(wordPrefab.gameObject, transform) as GameObject;
        TextOverlay overlay = instance.GetComponent<TextOverlay>();
        overlay.GetComponent<RectTransform>().sizeDelta = new Vector2(
                                                                      overlay.GetComponent<RectTransform>().sizeDelta.x,
                                                                      overlay.GetComponent<RectTransform>().sizeDelta.y * (definitionFontSize / wordFontSize)
                                                                     );
        if (defOverlays.Count >= maxWords) {
            TextOverlay defToRemove = defOverlays.Dequeue();
            Destroy(defToRemove.gameObject);
        }
        defOverlays.Enqueue(overlay);
        overlay.InitializeText(def, definitionFontSize);
    }

    public void AppearWords() {
        foreach (TextOverlay word in wordOverlays) {
            if (word) {
                word.Appear();
            }
        }
    }

    public void DisappearWords() {
        foreach (TextOverlay word in wordOverlays) {
            if (word) {
                word.Disappear();
            }
        }
    }

    public void AppearDefinitions() {
        foreach (TextOverlay def in defOverlays) {
            if (def) {
                def.Appear();
            }
        }
    }

    public void DisappearDefinitions() {
        foreach (TextOverlay def in defOverlays) {
            if (def) {
                def.Disappear();
            }
        }
    }
}
