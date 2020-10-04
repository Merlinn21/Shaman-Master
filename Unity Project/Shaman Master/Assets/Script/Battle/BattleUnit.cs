using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    public GhostBase ghostBase;
    public int level;

    public Ghost Ghost { get; set; } //properties

    public void Setup()
    {
        Ghost = new Ghost(ghostBase, level);

        GetComponent<Image>().sprite = Ghost.Base.getSprite();
    }
}
