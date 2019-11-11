using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBomb : Placeable {
    public GameObject indicator;
    public override void PlaceOn(GameObject square, GameObject placingPlayer) {
        base.PlaceOn(square, placingPlayer);
        GridControl gc = square.transform.parent.gameObject.GetComponent<GridControl>();
        if (gc) {
            Explode(gc);
            Destroy(gameObject);
        }
    }
    void Explode(GridControl grid) {
        int cipher = Random.Range(Game.ascii_a, Game.ascii_z + 1);
        foreach (GameObject go in grid.grid) {
            GridSquare gs = go.GetComponent<GridSquare>();
            if (gs && gs.tile) {
                LetterTile lt = gs.tile.GetComponent<LetterTile>();
                if (lt) {
                    int changedLetter = lt.letter + cipher - Game.ascii_z;
                    lt.ChangeLetterSprite((char)changedLetter);
                    Instantiate(indicator, gs.tile.transform);
                }
            }
        }
    }
}
