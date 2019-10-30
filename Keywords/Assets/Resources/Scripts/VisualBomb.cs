using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualBomb : MonoBehaviour
{
    void Explode(GridControl grid)
    {
        foreach (GameObject gs in grid.grid)
        {
            GridSquare sq = gs.GetComponent<GridSquare>();
            sq.tile.GetComponent<LetterTile>().ChangeLetterSprite((char)Random.Range(97, 123));
        }
    }
}
