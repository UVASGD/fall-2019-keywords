using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollisionCheck : MonoBehaviour {

    private Transform doors;//door container object
    private List<PlayerInfo> playerInfo; //all the player info objects

    void Start() {
        playerInfo = new List<PlayerInfo>();
        doors = transform;
        for (int i = 1; i <= 4; i++) {
            GameObject playerI = GameObject.Find("Player" + i);
            if (playerI) {
                playerInfo.Add(playerI.GetComponent<PlayerInfo>());
            }
        }
    }
    public void GiveKey(int playerNum) {
        if (playerNum < 1 || playerNum > playerInfo.Count) {
            print("tried to give key to weird value of playerNum");
            return;
        }
        playerInfo[playerNum - 1].IncKeys();
        SetDoorCollisions(playerNum);
    }
    private void SetDoorCollisions(int playerNum) {
        PlayerInfo player = playerInfo[playerNum - 1];
        foreach (Transform child in doors) {
            Door door = child.gameObject.GetComponent<Door>();
            if (player.keys >= door.keyNum) {
                //				print ("Ayy");
                door.Unlock(playerNum);
                Physics2D.IgnoreCollision(player.gameObject.GetComponent<CircleCollider2D>(), door.GetComponent<BoxCollider2D>());
            } else {
                //				print ("yyA");
                Physics2D.IgnoreCollision(player.gameObject.GetComponent<CircleCollider2D>(), door.GetComponent<BoxCollider2D>(), false);
            }
        }
    }
}
