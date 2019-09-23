using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Words : MonoBehaviour {

    public string wordsFilePath; // where is the words file that I read from?

    public int minWordLength; //minimum word length allowed
    public int maxWordLength; //maximum word length allowed
    public int maxWordLengthForScoring; //maximum word length taken into consideration when determining how many words can be made from a set of letters

    private Dictionary<char, int> characterFrequencies = new Dictionary<char, int> {
        {'e',37902},
        {'s',30209},
        {'a',26651},
        {'r',23054},
        {'i',22785},
        {'o',19721},
        {'l',17828},
        {'n',17826},
        {'t',17684},
        {'d',13635},
        {'u',12276},
        {'c',11174},
        {'p',9792},
        {'g',9593},
        {'m',9395},
        {'h',8073},
        {'b',7666},
        {'y',6601},
        {'k',5159},
        {'f',5153},
        {'w',4144},
        {'v',3043},
        {'z',1589},
        {'x',1150},
        {'j',1052},
        {'q',575}
    };
    private const int sumOfCharacterFrequencies = 323730;
    private const int numLettersInSource = 7;
    string[] words;//all words in the dictionary file
    string[] numletterwords;//all words of exactly numLettersInSource letters in length
    string[] currentSourceWords;//a selection of words which each floor in the dungeon will be based on
    public int numLevels;//how many levels in the dungeon
    List<string> currentLevelWords;//all words it's possible to make with the letters of the current source word
    List<string> unmadeLevelWords;//all words from currentLevelWords that have not been made yet (by anybody)
    List<string> madeLevelWords;//all words from currentLevelWords that have been made (by somebody).
    List<string>[] madeLevelWordsForEachPlayer;
    List<char> currentSourceChars;//all chars in the current source word.
    [HideInInspector]
    public int levelScore;//how fertile are the characters in the level?
    public float humanKnowledgeFactor = 0.7f; //approximately what percentage of words less than 8 letters long does the average player actually know?

    private AudioSource GetKeySFX;
    private AudioSource AlreadyMadeWordSFX;

    void Awake() {
        words = File.ReadAllLines(wordsFilePath);
        words = words.Where(w => w.Length >= minWordLength && w.Length <= maxWordLength).ToArray();
        numletterwords = GetNumLetterWords();
        currentSourceWords = GetSomeSourceWords(numLevels, 75, 250);
        currentSourceChars = new List<char>();
        madeLevelWords = new List<string>();
        madeLevelWordsForEachPlayer = new List<string>[4];
        for (int i = 0; i < 4; i++) {
            madeLevelWordsForEachPlayer[i] = new List<string>();
        }
        UpdateLevelWords(0);
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        GetKeySFX = GameObject.Find("GetKeySFX").GetComponent<AudioSource>();
        AlreadyMadeWordSFX = GameObject.Find("AlreadyMadeWordSFX").GetComponent<AudioSource>();
    }

    public void UpdateLevelWords(int level) {
        madeLevelWords.Clear();
        currentLevelWords = GetWords(currentSourceWords[level]);
        levelScore = currentLevelWords.Count;
        unmadeLevelWords = new List<string>(currentLevelWords);
        char[] sourceChars = currentSourceWords[level].ToCharArray();
        currentSourceChars.Clear();
        foreach (char c in sourceChars) {
            if (!currentSourceChars.Contains(c)) {
                currentSourceChars.Add(c);
            }
        }
    }

    string[] GetNumLetterWords() {
        List<string> result = new List<string>();
        foreach (string word in words) {
            if (word.Length == numLettersInSource) {
                result.Add(word);
            }
        }
        return result.ToArray();
    }

    public List<string> GetWords(string letters) {
        List<string> result = new List<string>();
        foreach (string word in words) {
            int wordlength = word.Length;
            if (wordlength >= minWordLength && wordlength <= maxWordLength) {
                int c = 0;
                bool done = false;
                while (c < word.Length && !done) {
                    if (!letters.Contains(word.Substring(c, 1))) {
                        done = true;
                    }
                    c++;
                }
                if (!done) {
                    result.Add(word);
                }
            }
        }
        return result;
    }

    public int GetScore(string letters) {
        return GetWords(letters).Where(w => w.Length <= maxWordLengthForScoring).ToList().Count;
    }

    public List<string> GetWordsExact(string letters) {
        List<string> result = new List<string>();
        string lettersRemaining = "";
        foreach (string word in currentLevelWords) {
            lettersRemaining = letters;
            int c = 0;
            bool done = false;
            while (c < word.Length && !done) {
                string sub = word.Substring(c, 1);
                if (!lettersRemaining.Contains(sub)) {
                    done = true;
                } else {
                    lettersRemaining = lettersRemaining.Remove(lettersRemaining.IndexOf(sub), 1);
                }
                c++;
            }
            if (!done) {
                result.Add(word);
            }
        }
        return result;
    }

    public int GetScoreExact(string letters) {
        return GetWordsExact(letters).Count;
    }

    public char GetRandomSourceChar() {
        return currentSourceChars[Random.Range(0, currentSourceChars.Count)];
    }

    //if not global grid:
    //	if owner's made words contains word:
    //		return false
    //	else if word is valid:
    //		add word to owner's words
    //		add word to global words
    //		return true
    //	else:
    //		return false
    //else:
    //	if global words contains word:
    //		return false
    //	else if word is valid:
    //		add word to maker's words
    //		add word to global words
    //		return true
    public bool ValidateWord(string word, int playerNum, bool globalGrid = false) {
        if (playerNum < 1 || playerNum > 4) {
            print("ValidateWord called on weird player num - returning false");
            return false;
        }
        List<string> madeWords = madeLevelWordsForEachPlayer[playerNum - 1];
        if (!globalGrid && madeWords.Contains(word)) {
            //			AlreadyMadeWordSFX.Play ();
            return false;
        }
        if (globalGrid && madeLevelWords.Contains(word)) {
            //			AlreadyMadeWordSFX.Play ();
            return false;
        }
        if (currentLevelWords.Contains(word)) {
            unmadeLevelWords.Remove(word);
            madeWords.Add(word);
            madeLevelWords.Add(word);
            GetKeySFX.Play();
			GameManager.GetWordOverlayHandler(playerNum).CreateWord(word);
            return true;
        }
        return false;
    }

    string[] GetSomeSourceWords(int howMany, int lowerThreshold, int upperThreshold) {
        List<string> result = new List<string>();
        for (int i = 0; i < howMany; i++) {
            string randomword = numletterwords[Random.Range(0, numletterwords.Length)];
            int score = GetScore(randomword);
            while (score < lowerThreshold || score > upperThreshold) {
                randomword = numletterwords[Random.Range(0, numletterwords.Length)];
                score = GetScore(randomword);
            }
            print(randomword + score);
            result.Add(randomword);
        }
        return result.ToArray();
    }

    public string GetRandomUnmadeWord() {
        return unmadeLevelWords[Random.Range(0, unmadeLevelWords.Count)];
    }
}
