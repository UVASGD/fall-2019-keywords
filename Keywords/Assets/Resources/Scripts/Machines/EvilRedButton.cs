using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilRedButton : Machine {

    bool toggled;

    protected override void PerformMachineAction() {
        //get tile
        GameObject tile = slot.GetComponent<GridSquare>().tile;
        //destroy tile
        Destroy(tile);
        slot.GetComponent<GridSquare>().tile = null;
        //TODO: if not toggled already, freeze all tiles in place on all grids and toggled = true
        //if toggled already, unfreeze all tiles on all grids and toggled = false
        //TODO: keep track of which player has activated the machine. Each player can only activate it twice

        base.PerformMachineAction();
    }
}
