using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DialogueManager : InteractableCanvas {
    public TMP_Text NameText; //The UI property where the name will be put
    public TMP_Text DialogueText; //The UI property where the text will be put (dialogue)

    private Queue<Dialogue> Sentenses; //The sentences of a dialogue

    public Animator DiaAnim;

    bool IsSpeaking;

    /* Initializes the dialogue, entering the name, pops up the UI etc. */
    public void StartDialogue(List<Dialogue> Dia) {
        IsSpeaking = true;

        Sentenses.Clear();

        foreach (Dialogue sentence in Dia)
            Sentenses.Enqueue(sentence);
        DiaAnim.SetBool("IsInDialogue", true);
        DisplayNextSentence();
    }

    /* The method that displays the next sentence when the player hits the next button. */
    public void DisplayNextSentence() {
        if (Sentenses.Count == 0) { //There are no more things to show, so close
            EndDialogue();
            return;
        }

        Dialogue sentence = Sentenses.Dequeue();
        NameText.text = sentence.getName();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence.getAnswer()));
    }

    /* A function that makes the sentence show up letter by letter. */
    IEnumerator TypeSentence(string sentence) {
        DialogueText.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            DialogueText.text += letter;
            yield return new WaitForSeconds(.03f);
        }
    }

    /* A function that close the UI of the dialogue, meaning the dialogue has ended. */
    public void EndDialogue() {
        IsSpeaking = false;

        DiaAnim.SetBool("IsInDialogue", false);
        StopAllCoroutines();
        StartCoroutine(waiter());
    }

    protected override void InitializeCanvas() {
        Sentenses = new Queue<Dialogue>();
        IsSpeaking = false;
    }

    IEnumerator waiter() {

        yield return new WaitForSeconds(.5f);


        Close();
    }

    protected override void OpenCanvas() {
        if(Inter == null || IsSpeaking) return;

        DialogueInteraction diaint = (DialogueInteraction)Inter;
        StartDialogue(diaint.CurrentDialogue());
    }

    public void OpenCanvas(List<Dialogue> dialogs) {
        if(IsSpeaking) return;
        Open();
        StartDialogue(dialogs);
    }
}
