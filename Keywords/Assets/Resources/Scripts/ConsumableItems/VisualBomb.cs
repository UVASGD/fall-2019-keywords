using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBomb : Placeable {
    public GameObject indicator;
    AudioSource SwapBombSFX;
    private bool onGrid = false;
    public float fuseTime = 20f;
    private Cooldown fuse;
    private GridControl gc;

    public override void PlaceOn(GameObject square, GameObject placingPlayer) {
        SwapBombSFX = GameManager.instance.sfx["SwapBombSFX"];
        base.PlaceOn(square, placingPlayer);
        onGrid = true;
        gc = square.transform.parent.gameObject.GetComponent<GridControl>();
        if (fuse == null) {
            fuse = new Cooldown(fuseTime);
        }
        fuse.Start();
        SwapBombSFX.Play();
    }

    public override void TakeFrom(GameObject square, GameObject takingPlayer)
    {
        onGrid = false;
        base.TakeFrom(square, takingPlayer);
    }

    // Update is called once per frame
    void Update() {
        if (onGrid && fuse.Ready() && gc) {
            Explode(gc);
            Destroy(gameObject);
        }
    }

    void Explode(GridControl grid) {
        SwapBombSFX.Play();
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
