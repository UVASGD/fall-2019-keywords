using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Flag : Placeable {
    public int ownerNum;
    private SpriteRenderer flagSprite;

    // Use this for initialization
    void Awake() {
        ownerNum = 0;
        flagSprite = transform.Find("FlagSprite").GetComponent<SpriteRenderer>();
    }

    // when the flag is picked up
    public void PickFlag(int newOwnerNum, GameObject owner) {
        // set the ownership of the flag to the player who picked it up
        ownerNum = newOwnerNum;
        flagSprite.color = owner.GetComponent<SpriteRenderer>().color;
    }

    public override void PlaceOn(GameObject square, GameObject placingPlayer) {
        base.PlaceOn(square, placingPlayer);
        bool claiming = square.transform.parent.gameObject.GetComponent<GridControl>().claimable;
        if (claiming) {
            square.transform.parent.gameObject.GetComponent<GridControl>().SetOwnership(ownerNum, placingPlayer);
            Color ownerColor = placingPlayer.GetComponent<SpriteRenderer>().color;
            float d = 0.7f;
            Color darkerColor = new Color(ownerColor.r * d, ownerColor.g * d, ownerColor.b * d, 1f);
            square.transform.parent.gameObject.GetComponent<GridControl>().StartRecoloring(ownerColor, darkerColor, square);
            Destroy(gameObject);
        }
    }


}
