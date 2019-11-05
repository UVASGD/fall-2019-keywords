using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {
    public virtual void PlaceOn(GameObject square, GameObject placingPlayer) {
        transform.SetParent(square.transform);
        transform.position = square.transform.position;
        Game.RepositionHeight(gameObject, Height.OnGridSquare);
        square.GetComponent<GridSquare>().SetTile(gameObject);
    }

    public virtual void TakeFrom(GameObject square, GameObject takingPlayer) {
    }
}
