using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCollisionCheck : MonoBehaviour {

	public Transform doors;//door container object
	public int keys;//how many keys does the player have?
	public Text keyUI;

	void Start(){
		keys = 0;
	}
	public void AddKey(){
		keys++;
		keyUI.text = keys.ToString ();
		SetDoorCollisions ();
	}
	private void SetDoorCollisions(){
		foreach (Transform child in doors) {
			Door door = child.gameObject.GetComponent<Door> ();
			if (keys >= door.keyNum) {
				//				print ("Ayy");
				door.Unlock(GetComponent<PlayerInfo>().playerNum);
				Physics2D.IgnoreCollision (GetComponent<CircleCollider2D> (), door.GetComponent<BoxCollider2D> ());
			}
			else {
				//				print ("yyA");
				Physics2D.IgnoreCollision (GetComponent<CircleCollider2D> (), door.GetComponent<BoxCollider2D> (),false);
			}
		}
	}
}
