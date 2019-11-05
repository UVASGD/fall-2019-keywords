using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {
    public virtual void PlaceOn(GameObject square, GameObject placingPlayer) {
    }

    public virtual void TakeFrom(GameObject square, GameObject takingPlayer) {
    }
}
