using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGrid : MonoBehaviour {
    public int width;
    public float borderSize;
    public GameObject gridSquare;

    // Use this for initialization
    void Awake() {
        transform.localScale = new Vector3(1f, 1f, 1f);
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < width; j++) {
                GameObject newSquare = GameObject.Instantiate(
                    gridSquare,
                    transform.position + new Vector3(j * (gridSquare.transform.localScale.x + borderSize), -i * (gridSquare.transform.localScale.x + borderSize), 0f),
                    Quaternion.identity,
                    transform
                );
                newSquare.name = "Square_" + i + "_" + j;
                GetComponent<GridControl>().grid[i, j] = newSquare;
                newSquare.GetComponent<GridSquare>().x = i;
                newSquare.GetComponent<GridSquare>().y = j;
            }
        }
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
