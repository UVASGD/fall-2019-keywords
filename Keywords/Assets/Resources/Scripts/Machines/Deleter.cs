using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : Machine {
    protected override void PerformMachineAction() {
        //delete tile
        GameObject tile = slot.GetComponent<GridSquare>().tile;
        Destroy(tile);
        slot.GetComponent<GridSquare>().tile = null;
    }
}