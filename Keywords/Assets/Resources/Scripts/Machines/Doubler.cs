using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doubler : Machine {
    private Vector3 placePosition;
    public GameObject Tile;
    private Transform TileContainer;

    protected override void Start() {
        base.Start();
        placePosition = new Vector3(0.5f, 0, 0);
        TileContainer = GameObject.Find("Tiles").transform;
    }
    protected override void PerformMachineAction() {
        //duplicate tile
        GameObject tile = slot.GetComponent<GridSquare>().tile;
        tile.GetComponent<LetterTile>().SetLifespan(tile.GetComponent <LetterTile>().lifespan * 2);
        Vector3 pos = transform.position + placePosition;

    }
}
