using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class Dialogue{
    [Header("Speaker:")]
    public string Name;

    [Header("Dialogue:")]
    [TextArea(2, 5)]
    public string Answer;

    public Dialogue(string sentAnswer) {
        Name = GameHandler.PlayerName;
        Answer = sentAnswer;
    }

    public Dialogue(string setName, string setAnswer) {
        if (setName == "*") setName = "";
        switch(setName) {
            case "*": setName = "";
                break;
            case "**":
                setName = GameHandler.PlayerName;
                break;
        }
        Name = setName;
        Answer = setAnswer;
    }

    public string getName() {
        if (Name == "**") return "";

        if (Name == "*") return GameHandler.PlayerName;

        return Name;
    }

    public string getAnswer() {
        if (Answer.Contains("*"))
            Answer = Answer.Replace("*", GameHandler.PlayerName);

        if(Answer.Contains("**"))
            Answer = Answer.Replace("**", "*");

        if (Answer.Length > 309) return Answer.Substring(0, 309);
        return Answer;
    }

    public override string ToString() {
        return Name + " | " + Answer;
    }
}
