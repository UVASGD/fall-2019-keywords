using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : Machine {

    protected override void PerformMachineAction() {
        //get tile
        GameObject tile = slot.GetComponent<GridSquare>().tile;
        base.PerformMachineAction();
        //TODO: unlock a random currently locked door, if one exists, for all players
    }
}
