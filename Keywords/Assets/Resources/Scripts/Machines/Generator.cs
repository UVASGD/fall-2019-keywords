using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Machine {
    protected override void PerformMachineAction() {
        //find tile
        GameObject tile = slot.GetComponent<GridSquare>().tile;
        //cost: delete tile
        Destroy(tile);
        slot.GetComponent<GridSquare>().tile = null;
        //TODO: find all other machines (besides this one) and complete their cooldowns

        base.PerformMachineAction();
    }
}
