using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleUnit[] enemies;
    [SerializeField] private BattleHud[] battleHuds;

    public event Action<bool> onBattleOver;

    public void SetupBattle()
    {
        enemies[0].Setup();
        enemies[1].Setup();
        enemies[2].Setup();

        battleHuds[0].SetData(enemies[0].Ghost);
        battleHuds[1].SetData(enemies[1].Ghost);
        battleHuds[2].SetData(enemies[2].Ghost);
    }

    public void StartBattle()
    {
        SetupBattle();
    }

    public void RunAway()
    {
        onBattleOver(true);
    }
}
