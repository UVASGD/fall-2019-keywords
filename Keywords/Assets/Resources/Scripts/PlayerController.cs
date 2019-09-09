using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private Inventory inventory;

    public GameObject activeSquare;//the grid square the player's currently on

    //controls
    private PlayerInfo me;
    private KeyCode LeftBumper;
    private KeyCode RightBumper;
    private KeyCode AButton;
    private KeyCode BButton;

    private float playerSpeed = 2.2f;
    const float pickupRadius = 0.2f; //how far away can the player pick up an object?

    private int playerNum;
    private int keyboardControlledPlayer = 1; //for debug / testing without controllers - one player can be controlled by the keyboard at a time;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        me = GetComponent<PlayerInfo>();
        playerNum = me.playerNum;
        SetControls();
    }

    // Update is called once per frame
    void Update() {
        //movement
        rb.velocity = playerSpeed * new Vector2(me.GetAxis("Horizontal"), me.GetAxis("Vertical"));
        //make keyboardControlledPlayer also controllable by keyboard
        if (playerNum == keyboardControlledPlayer) {
            rb.velocity = playerSpeed * new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        ////Interact with world
        //if (Input.GetKeyDown(AButton) || (me.playerNum == keyboardControlledPlayer && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)))) {
        //    Interact();
        //} else if (Input.GetKeyDown(BButton) || (me.playerNum == keyboardControlledPlayer && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.R)))) {
        //    Drop();
        //}

        ////Change which item is active
        //if (Input.GetKeyDown(LeftBumper) || (me.playerNum == keyboardControlledPlayer && Input.GetKeyDown(KeyCode.LeftArrow))) {
        //    Inventory.DecSlot();
        //} else if (Input.GetKeyDown(RightBumper) || (me.playerNum == keyboardControlledPlayer && Input.GetKeyDown(KeyCode.RightArrow))) {
        //    Inventory.IncSlot();
        //}
        //make keyboardControlledPlayer adjustable by keyboard
        if (me.playerNum == keyboardControlledPlayer) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                keyboardControlledPlayer = 1;
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                keyboardControlledPlayer = 2;
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                keyboardControlledPlayer = 3;
            } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                keyboardControlledPlayer = 4;
            }
        }
    }

    private void SetControls() {
        AButton = me.GetKeyCode("A");
        BButton = me.GetKeyCode("B");
        LeftBumper = me.GetKeyCode("LeftBumper");
        RightBumper = me.GetKeyCode("RightBumper");
    }

    public void SetActiveSquare(GameObject newSquare) {
        activeSquare = newSquare;
    }

    //pseudocode of this:
    /*
	x = is player hovering over a grid square?
	y = is player currently holding something in inventory?
	z = is player holding a letter tile?
	w = is there a letter tile on the grid square?


	x y z w : swap inventory item with thing on the square
	x y z !w : place inventory item on the square 
	x y !z !w : Perform item’s action
	x !y !z w : take tile on square into inventory
	x !y !z !w : normal grab
	!x y !z !w : Perform item’s action
	!x !y !z !w : normal grab

	all other combinations are impossible or should do nothing
	 */
    //private void Interact() {
    //    //		print ("interacting");
    //    bool x = (activeSquare != null);
    //    bool y = (items[inventorySlot] != null);
    //    bool z = y ? items[inventorySlot].CompareTag("LetterTile") : false;
    //    bool w = x ? activeSquare.GetComponent<GridSquare>().tile != null : false;
    //    //		print (x + " " + y + " " + z + " " + w);

    //    if (!y && !z && !w) {
    //        NormalGrab();
    //    } else if (y && !z && !w) {
    //        PerformItemAction();
    //    } else if (x) {
    //        if (y && z) {
    //            if (w) {
    //                SwapWithSquare();
    //            } else {
    //                PlaceOnSquare();
    //            }
    //        } else if (!y && !z && w) {
    //            TakeFromSquare();
    //        }
    //    }
    //}
}
