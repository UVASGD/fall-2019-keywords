using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour {
    public GameObject tile;
    //	public char letter;
    public Color highlightedColor;
    [HideInInspector]
    public Color normalColor;
    private SpriteRenderer sr;
    public int x;
    public int y;
    void Awake() {
        tile = null;
        sr = GetComponent<SpriteRenderer>();
        normalColor = sr.color;
    }
    //TODO: make it rigorously impossible for there to be no active grid square if the player is within the confines of a grid
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Contains("Player") && other.isTrigger) {
            sr.color = highlightedColor;
            other.transform.gameObject.GetComponent<PlayerController>().SetActiveSquare(gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name.Contains("Player") && other.isTrigger) {
            sr.color = normalColor;
            other.transform.gameObject.GetComponent<PlayerController>().SetActiveSquare(null);
        }
    }

    public char GetLetter() {
        if (tile == null) {
            return transform.parent.gameObject.GetComponent<GridControl>().placeholder;
        }
        return tile.GetComponent<LetterTile>().letter;
    }

    public void SetTile(GameObject newTile) {
        if (newTile != null && !newTile.CompareTag("LetterTile")) {
            print("tried to set tile to something not tagged as LetterTile");
        }
        tile = newTile;
    }
}
