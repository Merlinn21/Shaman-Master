using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost
{
    GhostBase _base;
    int lvl;

    public Ghost(GhostBase ghostBase, int level)
    {
        _base = ghostBase;
        lvl = level;
    }

    public int MaxHp()
    {
        return Mathf.FloorToInt(_base.getMaxHp());
    }

    public int Attack()
    {
        return Mathf.FloorToInt(_base.getAtk());
    }

    public int Defend()
    {
        return Mathf.FloorToInt(_base.getDef());
    }

    public int Speed()
    {
        return Mathf.FloorToInt(_base.getSpeed());
    }
}
