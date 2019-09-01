using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTile : MonoBehaviour {
	public char letter;//what is my letter?
	public int matches;//how many matches can you make with me?
	private GameObject letterSprite;
	private GameObject numberSprite;

	private Words words;

	void Awake(){
		words = GameObject.Find ("GM").GetComponent<Words> ();
		letterSprite = transform.GetChild (0).gameObject;
		numberSprite = transform.GetChild (1).gameObject;
//		SetLetter (words.GetRandomSourceChar ());
//		SetLetter ((char)Random.Range (97, 123));
//		SetMatches (Random.Range(3,9));
//		Dec ();

	}
	public void SetLetter(char newletter){
		if((int)newletter < 97 || (int)newletter > 122){//not in a-z
			print("tried to set letter tile to weird char");
			return;
		}
		letter = newletter;
		string spriteName = "LetterSprites/" + letter.ToString ();
		letterSprite.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (spriteName);
	}

	public void SetMatches(int newmatches){
		if (newmatches < 0 || newmatches > 16) {//out of range
			print("tried to set letter tile to weird number");
			return;
		}
		if (newmatches == 0) {
//			print ("tile dead");
			GridSquare gs = transform.parent.gameObject.GetComponent<GridSquare> ();
			if (gs != null) {
				gs.SetTile (null);
			}
			Destroy (gameObject);
			return;
		}
		matches = newmatches;
		string spriteName = "NumberSprites/" + matches.ToString ();
		if (matches == 16) {
			spriteName = "NumberSprites/inf";
		}
		numberSprite.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (spriteName);
	}

	//decrement
	public void Dec(){
		if (matches != 16) {
			SetMatches (matches - 1);
		}
	}
}