using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private Rigidbody2D rb;
    private PlayerInfo me;

    private float playerSpeed = 2.2f;
    private int playerNum;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        me = GetComponent<PlayerInfo>();
        playerNum = me.playerNum;
    }

    // Update is called once per frame
    void Update() {
        rb.velocity = playerSpeed * new Vector2(me.GetAxis("Horizontal"), me.GetAxis("Vertical"));
        //make player 1 also controllable by keyboard
        //if (playerNum == 1) {
        //    rb.velocity = playerSpeed * new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //}
    }
}
