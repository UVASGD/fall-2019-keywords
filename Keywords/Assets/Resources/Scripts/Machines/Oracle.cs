using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oracle : Machine {
    [HideInInspector]
    public Words words = GameManager.words;
    WordDisplayer word_displayer;

    private void Awake()
    {
        word_displayer = GetComponentInChildren<WordDisplayer>();
    }

    protected override void PerformMachineAction() {
        //delete tile
        GameObject tile = slot.GetComponent<GridSquare>().tile;
        Destroy(tile);
        slot.GetComponent<GridSquare>().tile = null;
        //TODO: make piece of paper object with this word instead of printing
        print(words.GetRandomUnmadeWord());
        word_displayer.DisplayWord(words.GetRandomUnmadeWord());
    }
}