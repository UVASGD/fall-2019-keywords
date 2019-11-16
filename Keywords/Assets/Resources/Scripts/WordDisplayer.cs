using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordDisplayer : MonoBehaviour {
    TextMeshPro word_display;
    Animator anim;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        word_display = GetComponent<TextMeshPro>();
    }

    public void DisplayWord(string word) {
        word_display.text = word.ToUpper();
        anim.Play("default", -1, 0f);
        anim.SetTrigger("Display");
    }
}
