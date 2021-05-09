using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    public Transform tipOfAxe;
    public float hitboxRadius;
    public LayerMask trees;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AxeSwung()
    {
        //here
        //Debug.Log("Axe hit");
        /*
        if (Physics.CheckSphere(tipOfAxe.position, hitboxRadius, trees))
        {
            Debug.Log("Hit the tree");
        }
        */
        Collider[] hitColliders = Physics.OverlapSphere(tipOfAxe.position, hitboxRadius, trees);
        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Tree")
            {
                TreeBehavior treeBehavior = hitCollider.gameObject.GetComponent<TreeBehavior>();
                treeBehavior.treeHit();
            }
        }

    }
    public void PlaySwingSound()
    {
        audioSource.Play();
    }
}
