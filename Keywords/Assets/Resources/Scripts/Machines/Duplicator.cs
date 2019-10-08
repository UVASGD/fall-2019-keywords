using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : Machine {
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
        Vector3 pos = transform.position + placePosition;
        GameObject newTile = Instantiate(Tile, pos, Quaternion.identity, TileContainer);
        newTile.GetComponent<LetterTile>().SetLetter(tile.GetComponent<LetterTile>().letter);
        newTile.GetComponent<LetterTile>().SetLifespan(tile.GetComponent<LetterTile>().lifespan);

        //cost: 1 tile lifespan
        tile.GetComponent<LetterTile>().DecLifespan();
    }
}