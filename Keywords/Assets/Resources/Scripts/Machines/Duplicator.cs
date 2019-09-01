using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : Machine {
	private Vector3 placePosition;
	public GameObject Tile;
	private Transform TileContainer;

	protected override void Start(){
		base.Start ();
		placePosition = new Vector3 (0.5f, 0, 0);
		TileContainer = GameObject.Find ("Tiles").transform;
	}
	protected override void PerformMachineAction(){
		//duplicate tile
		GameObject tile = slot.GetComponent<GridSquare>().tile;
		Vector3 pos = transform.position + placePosition;
		GameObject newTile = GameObject.Instantiate (Tile, pos, Quaternion.identity, TileContainer);
		newTile.GetComponent<LetterTile> ().SetLetter (tile.GetComponent<LetterTile> ().letter);
		newTile.GetComponent<LetterTile> ().SetMatches (tile.GetComponent<LetterTile> ().matches);
	}
}