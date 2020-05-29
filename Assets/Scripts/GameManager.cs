using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Random = UnityEngine.Random;

public enum TextMode { Normal, Reverse, Scramble }
public class GameManager : MonoBehaviour {
    //INSTANCE VARIABLES
    public InputField input, output;
    public ToggleGroup toggles;

    private TextMode _textMode;

    //runs when program starts
    private void Start() {
        _textMode = TextMode.Normal;
    }

    
    //changes the output text box depending on the current TextMode
    public void ChangeOutput() {
        var value = input.text;
        switch (_textMode) {
            case TextMode.Reverse:
            output.text = Reverse(value);
            break;
            case TextMode.Scramble:
            output.text = Scramble(value);
            break;
            case TextMode.Normal:
                output.text = value; 
            break;
            default: 
            throw new ArgumentOutOfRangeException();
        }
    }

    //sets the text mode to Normal and update buttons
    public void TextModeNormal() {
        _textMode = TextMode.Normal;
        ChangeToggleText();
    }

    //sets the text mode to Reverse and update buttons
    public void TextModeReverse() {
        _textMode = TextMode.Reverse;
        ChangeToggleText();
    }

    //sets the text mode to Scramble and update buttons
    public void TextModeScramble() {
        _textMode = TextMode.Scramble;
        ChangeToggleText();
    }

    //copies the text inside the output text box to clipboard
    public void CopyOutputToClipboard() {
        var textEditor = new TextEditor {text = output.text};
        textEditor.SelectAll();
        textEditor.Copy();
    }

    //reverses the word
    private static string Reverse(string word) {
        var charArray = word.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    
    //scrambles the string word
    private static string Scramble(string word) 
    { 
        var chars = new char[word.Length];
        var index = 0; 
        while (word.Length > 0) 
        {
            var next = Random.Range(0, word.Length - 1);
            chars[index] = word[next];
            word = word.Substring(0, next) + word.Substring(next + 1); 
            ++index; 
        } 
        return new string(chars); 
    }  

    //updates the button text color depending on its state
    private void ChangeToggleText() {
        foreach (Transform child in toggles.transform)
            child.Find("Label").GetComponent<Text>().color = child.GetComponent<Toggle>().isOn ? Color.black : Color.white;
    }
}
