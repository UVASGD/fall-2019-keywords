using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbWaiter : Machine {

    protected override void PerformMachineAction() {
        //get tile
        GameObject tile = slot.GetComponent<GridSquare>().tile;
        //TODO: pass this tile down to next level (or something similar)

        base.PerformMachineAction();
    }
}
