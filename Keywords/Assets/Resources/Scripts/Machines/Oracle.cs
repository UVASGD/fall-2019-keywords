using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oracle : Machine {
    [HideInInspector]
    public Words words;
    WordDisplayer word_displayer;

    private void Awake() {
        word_displayer = transform.Find("WordDisplayer").GetComponent<WordDisplayer>();
    }

    protected override void Start() {
        base.Start();
        words = GameManager.words;
    }

    protected override void PerformMachineAction() {
        //delete tile
        GameObject tile = slot.GetComponent<GridSquare>().tile;
        int tileLifespan = tile.GetComponent<LetterTile>().lifespan;
        word_displayer.DisplayWord(words.GetRandomUnmadeWord(Mathf.Clamp(tileLifespan, 4, 10)));
        tile.GetComponent<LetterTile>().Die();
        slot.GetComponent<GridSquare>().tile = null;

        base.PerformMachineAction();
    }
}
