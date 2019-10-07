using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterConverter : Machine
{
    private char nextLetter(char letter)
    {
        if(letter == 'z')
        {
            return 'a';
        }
        return (char)(letter + 1);
    }
    protected override void PerformMachineAction()
    {
        //delete tile
        LetterTile tile = slot.GetComponent<GridSquare>().tile.GetComponent<LetterTile>();
        char newTileChar = nextLetter(tile.letter);
        tile.GetComponent<LetterTile>().SetLetter(newTileChar);
    }
}
