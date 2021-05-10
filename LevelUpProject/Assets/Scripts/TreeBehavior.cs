using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehavior : MonoBehaviour
{
    int hp = 8;
    public AudioSource audioSource;

    public Transform[] branchSpawnPoints;
    public GameObject branch;
    public GameObject fallSoundObj;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void treeHit()
    {
        hp -= 1;
        if (hp <= 0)
        {
            Debug.Log("tree dead");
            foreach (var spawnPoint in branchSpawnPoints)
            {
                Instantiate(branch, spawnPoint.position, Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
            }

            Instantiate(fallSoundObj, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            animator.SetTrigger("hitTree");
            audioSource.Play();
        }
    }
}
