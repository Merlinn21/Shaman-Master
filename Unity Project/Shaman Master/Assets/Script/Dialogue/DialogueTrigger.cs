using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    [SerializeField] private GameObject speakerLeft;
    [SerializeField] private GameObject speakerRight;

    private DialogueUI UIleft;
    private DialogueUI UIright;

    private int activeLineIndex = 0;
    public event Action<bool> onDialogueOver;

    public void SetDialogue(Dialogue _dialogue)
    {
        dialogue = _dialogue;
    }

    public void StartDialogue()
    {
        UIleft = speakerLeft.GetComponent<DialogueUI>();
        UIright = speakerRight.GetComponent<DialogueUI>();

        UIleft.Speaker = dialogue.getCharLeft();
        UIright.Speaker = dialogue.getCharRight();

        DisplayLine();
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextLine();
    }

    public void NextLine()
    {
        if (activeLineIndex < dialogue.lines.Length)
        {
            DisplayLine();
            activeLineIndex++;
        }
        else if(dialogue.getFreeRoam())
        {
            UIleft.HideUI();
            UIright.HideUI();

            activeLineIndex = 0;

            onDialogueOver(true);
        }
        else if (!dialogue.getFreeRoam())
        {
            UIleft.HideUI();
            UIright.HideUI();

            activeLineIndex = 0;
            onDialogueOver(false);
        }
    }

    public void DisplayLine()
    {
        Line line = dialogue.lines[activeLineIndex];
        Character currChar = line.character;

        if (UIleft.SpeakerIs(currChar))
        {
            SetDialogue(UIleft, UIright, line.text);
        }
        else
        {
            SetDialogue(UIright, UIleft, line.text);
        }
    }

    private void SetDialogue(DialogueUI show, DialogueUI hide, string text)
    {
        show.Dialogue = text;
        show.ShowUI();

        hide.HideUI();
    }
}
