using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {
    public int ownerNum;
    private GameObject flagSprite;
    private GameObject owner;


    // Use this for initialization
    void Awake()
    {
        ownerNum = 0;
        // flagSprite = ;
    }

    // when the flag is picked up
    public void PickFlag(int newOwnerNum, GameObject owner)
    {
        // set the ownership of the tile to the player who owns the flag
        ownerNum = newOwnerNum;
        return;
    }


}
