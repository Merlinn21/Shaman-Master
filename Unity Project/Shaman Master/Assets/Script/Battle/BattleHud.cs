using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text ghostName;

    public void SetData(Ghost ghost)
    {
        ghostName.text = ghost.Base.getName();     
    }
}
