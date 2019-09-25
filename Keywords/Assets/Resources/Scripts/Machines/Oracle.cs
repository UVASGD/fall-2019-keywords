using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oracle : Machine {
    public Words words = GameManager.words;

    protected override void PerformMachineAction() {
        //delete tile
        GameObject tile = slot.GetComponent<GridSquare>().tile;
        Destroy(tile);
        slot.GetComponent<GridSquare>().tile = null;
        //TODO: make piece of paper object with this word instead of printing
        print(words.GetRandomUnmadeWord());
    }
}