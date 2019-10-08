using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doubler : Machine {
    protected override void PerformMachineAction() {
        GameObject tile = slot.GetComponent<GridSquare>().tile;
        tile.GetComponent<LetterTile>().SetLifespan(tile.GetComponent<LetterTile>().lifespan * 2);
    }
}
