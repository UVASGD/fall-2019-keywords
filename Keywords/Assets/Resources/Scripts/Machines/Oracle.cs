using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oracle : Machine {
	public Words words = GameObject.Find("GM").GetComponent<Words>();

	protected override void PerformMachineAction(){
		//delete tile
		GameObject tile = slot.GetComponent<GridSquare>().tile;
		Destroy (tile);
		slot.GetComponent<GridSquare> ().tile = null;
		print (words.GetRandomUnmadeWord ());
	}
}