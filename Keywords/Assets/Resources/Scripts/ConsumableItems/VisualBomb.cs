using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBomb : Placeable {
    public GameObject indicator;
    AudioSource VisualBombSFX;
    public override void PlaceOn(GameObject square, GameObject placingPlayer) {
        VisualBombSFX = GameManager.instance.sfx["VisualBombSFX"];
        base.PlaceOn(square, placingPlayer);
        GridControl gc = square.transform.parent.gameObject.GetComponent<GridControl>();
        if (gc) {
            Explode(gc);
            Destroy(gameObject);
        }
    }
    void Explode(GridControl grid) {
        VisualBombSFX.Play();
        int cipher = Random.Range(1, 26);
        foreach (GameObject go in grid.grid) {
            GridSquare gs = go.GetComponent<GridSquare>();
            if (gs && gs.tile) {
                LetterTile lt = gs.tile.GetComponent<LetterTile>();
                if (lt) {
                    int changedLetter = lt.letter + cipher;
                    if (changedLetter > Game.ascii_z)
                        changedLetter = changedLetter - 26;
                    lt.ChangeLetterSprite((char)changedLetter);
                    Instantiate(indicator, gs.tile.transform);
                }
            }
        }
    }
}
