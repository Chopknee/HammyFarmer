using System.Collections.Generic;
using UnityEngine;

class TestLineSplitter : MonoBehaviour {

    public string input;

    public void Start () {
        foreach (string line in SplitByLine(input, 50, true)) {
            Debug.Log(line + " " + line.Length);
        }
    }

    /**
     * Splits a string into lines based on the line length.
     *      Also keeps 'keywords' on a single line when defined by the following general format
     *      KeywordIdentifier[Text To Show] - the entire statement is moved to the next line if it
     *      goes over the count. The text ouside the brackets is not counted toward the final count.
     *      
     *      Also does auto capitalization of the next sentence.
     * 
     */
    List<string> SplitByLine ( string input, int lineLength, bool autoCapitalize ) {
        char[] characters = input.ToCharArray();
        List<string> lines = new List<string>();
        string[] words = input.Split(' ');

        int charsInLine = 0;
        int charsInWord = 0;

        string word = "";
        string line = "";

        bool ignoreSpaces = false;

        for (int i = 0; i < characters.Length; i++) {
            char letter = characters[i];
            charsInLine++;

            if (letter == '[') {
                ignoreSpaces = true;
                //Subtract out everything from the length of the word until now
                charsInLine -= word.Length + 1;
            }
            if (letter == ']') {
                ignoreSpaces = false;
            }

            //If the letter is not a space, OR if the letter is a space, but ignore spaces flag is set
            if (letter != ' ' || letter == ' ' && ignoreSpaces) {
                charsInWord++;
                word += letter;
            //if the letter is a space AND the ignore spaces flag is NOT set.
            } else if (letter == ' ' && !ignoreSpaces) {
                line += word + ' ';
                word = "";
                charsInWord = 0;
            }
            //Line break insertion
            if (charsInLine == lineLength) {
                //Split here
                lines.Add(line);
                line = "";
                charsInLine = 0;
            }

            //Auto capitalization
            if (letter == '.' && autoCapitalize) {
                //Grab the next char
                int nxt = i+1;
                //Look for the first character that isn't a space. Sadly if it is punctuation, this will break.
                while (nxt < characters.Length && characters[nxt] == ' ') {
                    nxt++;
                    if (characters[nxt] != ' ') {
                        characters[nxt] = char.ToUpper(characters[nxt]);//So fucking stoopid
                        break;
                    }
                }
            }

            //End of the road.
            if (i == characters.Length-1) {
                line += word;
                lines.Add(line);
            }
        }
        return lines;
    }



}
