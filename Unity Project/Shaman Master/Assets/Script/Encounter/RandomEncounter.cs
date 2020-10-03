using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    public LayerMask encounterLayer;
    public float chance;

    public void CheckEncounter(Vector3 pos)
    {
        if(Physics2D.OverlapCircle(pos, 0.2f, encounterLayer) != null)
        {
            if(Random.Range(1,101) <= chance)
            {
                Debug.Log("Battle");
            }
        }
    }
}
