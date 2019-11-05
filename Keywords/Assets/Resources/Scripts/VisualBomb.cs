using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBomb : Placeable {
    public override void PlaceOn(GameObject square, GameObject placingPlayer) {
        base.PlaceOn(square, placingPlayer);
        print("BOOM");
        Explode(square.transform.parent.gameObject.GetComponent<GridControl>());
        Destroy(gameObject);
    }
    void Explode(GridControl grid) {
        foreach (GameObject go in grid.grid) {
            GridSquare gs = go.GetComponent<GridSquare>();
            if (gs && gs.tile) {
                LetterTile lt = gs.tile.GetComponent<LetterTile>();
                if (lt) {
                    lt.ChangeLetterSprite((char)Random.Range(Game.ascii_a, Game.ascii_z + 1));
                }
            }
        }
    }
}
