using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterConverter : Machine {
    private char nextLetter(char letter) {
        if (letter == 'z') {
            return 'a';
        }
        return (char)(letter + 1);
    }
    protected override void PerformMachineAction() {
        LetterTile tile = slot.GetComponent<GridSquare>().tile.GetComponent<LetterTile>();
        char newTileChar = nextLetter(tile.letter);
        tile.SetLetter(newTileChar);

        //cost: 1 tile lifespan
        tile.DecLifespan();

        base.PerformMachineAction();
    }
}
