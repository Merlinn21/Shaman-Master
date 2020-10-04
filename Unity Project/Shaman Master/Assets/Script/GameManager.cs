using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    FreeRoam,
    Battle
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private GridMovement playerMove;
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private Camera mainCamera;

    GameState state;

    private void Start()
    {
        playerMove.onEncounter += StartBattle; // manggil fungsi StartBattle kalo onEncounter kepanggil
        battleSystem.onBattleOver += EndBattle;
    }

    public void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }
    
    public void EndBattle(bool win)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
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
    }
}
