using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchLightBranch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BranchMeshObj")
        {
            BranchBehavior bb = other.gameObject.GetComponent<BranchBehavior>();
            if (bb.isLit == true)
            {
                transform.parent.GetComponent<BranchBehavior>().isLit = true; //sets the parents bb behavior to lit so logs light other logs if lit
            }

        }
    }
}
