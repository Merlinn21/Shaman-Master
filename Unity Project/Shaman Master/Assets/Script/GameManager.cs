using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    FreeRoam,
    Battle,
    Dialogue
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private GridMovement playerMove;
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private Camera mainCamera;

    GameState state;

    private void Start()
    {
        playerMove.onEncounter += StartBattle; // manggil fungsi StartBattle kalo onEncounter kepanggil
        battleSystem.onBattleOver += EndBattle;
        playerMove.onDialogue += StartDialogue;
        dialogueTrigger.onDialogueOver += EndDialogue;
    }

    public void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();
    }
    
    public void EndBattle(bool win)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        state = GameState.Dialogue;
        dialogueTrigger.SetDialogue(dialogue);
        dialogueTrigger.StartDialogue();
    }

    public void EndDialogue(bool freeRoam)
    {
        if (freeRoam)
        {
            state = GameState.FreeRoam;

        }
        else
        {
            state = GameState.Battle;
            StartBattle();
        }
        
    }

    private void Update()
    {
        if(state == GameState.FreeRoam)
        {
            playerMove.HandleUpdate();
        }
        else if(state == GameState.Battle)
        {

        }
        else if(state == GameState.Dialogue)
        {
            dialogueTrigger.HandleUpdate();
        }
    }
}
