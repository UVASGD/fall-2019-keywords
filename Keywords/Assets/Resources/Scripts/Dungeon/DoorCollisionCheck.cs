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
                door.Unlock(playerNum);
                Physics2D.IgnoreCollision(player.gameObject.GetComponent<CircleCollider2D>(), door.GetComponent<BoxCollider2D>());
                
                // Create indicator that goes towards the door when it unlocks. If it's close or it's mid/late game
                if (player.keys > 13 || (door.transform.position - player.transform.position).magnitude < 15f) {
                    GameObject adoorable = Instantiate(Resources.Load("Prefabs/FX/GlowingOrbFX"), player.transform.position, Quaternion.identity) as GameObject;
                    adoorable.GetComponent<GoToDoor>().GoTo(door.transform);
                    string layerName = "P" + playerNum.ToString();
                    adoorable.layer = LayerMask.NameToLayer(layerName);
                }
            } else {
                //				print ("yyA");
                Physics2D.IgnoreCollision(player.gameObject.GetComponent<CircleCollider2D>(), door.GetComponent<BoxCollider2D>(), false);
            }
        }
    }
}
