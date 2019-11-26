using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTile : Placeable {
    public char letter;//what is my letter?
    public int lifespan;//how many more words can you make with me?
    private GameObject letterSprite;
    private GameObject numberSprite;

    private Words words;

    public bool infinite;

    private AudioSource DestroyTileSFX;


    Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
        words = GameManager.words;
        letterSprite = transform.FindDeepChild("LetterSprite").gameObject;
        numberSprite = transform.FindDeepChild("NumberSprite").gameObject;
        DestroyTileSFX = GameManager.instance.sfx["DestroyTileSFX"];
        //		SetLetter (words.GetRandomSourceChar ());
        //		SetLetter ((char)Random.Range (97, 123));
        //		SetMatches (Random.Range(3,9));
        //		Dec ();

    }

    public override void PlaceOn(GameObject square, GameObject placingPlayer) {
        base.PlaceOn(square, placingPlayer);
        int x = square.GetComponent<GridSquare>().x;
        int y = square.GetComponent<GridSquare>().y;
        if (square.transform.parent.gameObject.GetComponent<GridControl>()) {
            square.transform.parent.gameObject.GetComponent<GridControl>().ValidateWords(x, y, placingPlayer);
        }
    }

    public override void TakeFrom(GameObject square, GameObject takingPlayer) {
        base.TakeFrom(square, takingPlayer);
        int x = square.GetComponent<GridSquare>().x;
        int y = square.GetComponent<GridSquare>().y;
        if (square.transform.parent.gameObject.GetComponent<GridControl>()) {
            square.transform.parent.gameObject.GetComponent<GridControl>().ValidateWords(x, y, takingPlayer);
        }
    }

    public void SetLetter(char newletter) {
        if ((int)newletter < Game.ascii_a || (int)newletter > Game.ascii_z) {//not in a-z
            print("tried to set letter tile to weird char");
            return;
        }
        letter = newletter;
        string spriteName = "LetterSprites/" + letter.ToString();
        letterSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteName);

        Transform indicator;
        if (indicator = transform.Find("SwapBombIndicator(Clone)")) {
            Destroy(indicator.gameObject);
        }
    }

    public void ChangeLetterSprite(char c) {
        string spriteName = "LetterSprites/" + c.ToString();
        letterSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteName);
    }

    public void SetLifespan(int newLifespan) {
        if (newLifespan < 0) {//out of range
            print("tried to set letter tile to weird number");
            return;
        }
        lifespan = newLifespan;
        if (newLifespan == 0) {
            Die();
            return;
        }
        string spriteName = "NumberSprites/" + lifespan.ToString();
        if (lifespan >= 16) {
            lifespan = 16;
            spriteName = "NumberSprites/inf";
        }

        numberSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteName);
    }

    //decrement
    public void DecLifespan() {
        //TODO: call animation to flash this letter tile


        if (lifespan != 16) {
            SetLifespan(lifespan - 1);
        }
        if (lifespan == 0) {
            return;
        }

        Transform indicator;
        if (indicator = transform.Find("SwapBombIndicator(Clone)")) {
            Destroy(indicator.gameObject);
            ChangeLetterSprite(letter);
        }
        Animate();

    }

    public void Animate() {
        anim.SetTrigger("Flash");
    }
    public void Die() {
        //			print ("tile dead");
        GridSquare gs = transform.parent.gameObject.GetComponent<GridSquare>();
        if (gs != null) {
            gs.SetTile(null);
        }
        DestroyTileSFX.Play();
        anim.SetTrigger("Death");
        Destroy(this.gameObject, .45f);

        //Destroy(gameObject);
    }
}