using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchHeat : MonoBehaviour
{
    public float baseHeatGiven = 1f;
    public float heatGiven = 0;
    public BranchBehavior bB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bB.isLit == true)
            heatGiven = baseHeatGiven * transform.parent.localScale.x;
        else
            heatGiven = 0;
    }
}
