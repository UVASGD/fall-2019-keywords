using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public int keyNum; //how many keys does the player need to unlock the door?
    private AudioSource unlockDoorSFX;
    private bool[] locked;
    private GameObject lockedSprite;//child sprite object showing door as locked to players who haven't unlocked it
    private Transform keyNumText1;
    private Transform keyNumText2;

    void Start() {
        unlockDoorSFX = GameObject.Find("UnlockDoorSFX").GetComponent<AudioSource>();
        locked = new bool[4] { true, true, true, true };
        lockedSprite = transform.Find("LockedDoorSprite").gameObject;
        keyNumText1 = transform.Find("KeyNumText1");
        keyNumText1.rotation = Quaternion.identity;
        keyNumText1.GetComponent<TMPro.TextMeshPro>().text = keyNum.ToString();
        keyNumText2 = transform.Find("KeyNumText2");
        keyNumText2.rotation = Quaternion.identity;
        keyNumText2.GetComponent<TMPro.TextMeshPro>().text = keyNum.ToString();
        if (transform.rotation != Quaternion.identity) {
            keyNumText1.localScale = new Vector3(keyNumText1.localScale.y, keyNumText1.localScale.x, keyNumText1.localScale.z);
            keyNumText2.localScale = new Vector3(keyNumText2.localScale.y, keyNumText2.localScale.x, keyNumText2.localScale.z);
        }
    }

    public void Unlock(int playerNum) {
        if (locked[playerNum - 1] == true) {
            locked[playerNum - 1] = false;
            Game.SetInvis(lockedSprite, playerNum);
            unlockDoorSFX.Play();
        }
    }
}
