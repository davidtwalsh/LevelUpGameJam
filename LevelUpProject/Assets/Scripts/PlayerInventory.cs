using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject pickUpText;
    public GameObject axeLighterPickup;
    bool inRangeOfAxeAndLighter = false;
    public GameObject axe;
    int currentInventorySlot = 0;
    public Animator axeAnimator;
    public GameObject inventoryUI;
    public GameObject pickUpBranchText;
    List<GameObject> branches;
    public GameObject numberBranchesText;
    int numberBranches = 0;
    AudioSource audioSource;

    //For throwing sticks
    public GameObject branch;


    // Start is called before the first frame update
    void Start()
    {
        branches = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRangeOfAxeAndLighter == true && Input.GetKeyDown(KeyCode.E))
        {
            inRangeOfAxeAndLighter = false;
            currentInventorySlot = 1;
            axe.SetActive(true);
            axeLighterPickup.SetActive(false);
            pickUpText.SetActive(false);
            inventoryUI.SetActive(true);
        }

        //trigger axe swing animation
        if (currentInventorySlot == 1 && Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Axe Swing");
            axeAnimator.SetTrigger("playerChops");
        }

        //only show pick up branch text if char over a branch
        if (branches.Count > 0) { 
            pickUpBranchText.SetActive(true);
            //let user pick one up
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject br = branches[0];
                branches.RemoveAt(0);
                Destroy(br.transform.parent.gameObject);
                numberBranches += 1;
                audioSource.Play();
            }
        }
        else
            pickUpBranchText.SetActive(false);

        //update # branches text
        numberBranchesText.GetComponent<Text>().text = "Sticks: " + numberBranches;

        //Throw branch
        if (Input.GetKeyDown(KeyCode.Q) && numberBranches > 0)
        {
            GameObject br = Instantiate(branch, transform.position + transform.forward * 3, Quaternion.Euler(0f, Random.Range(0.0f, 360.0f), 0f));

            numberBranches -= 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AxeLighter")
        {
            pickUpText.SetActive(true);
            inRangeOfAxeAndLighter = true;
        }
        else if (other.gameObject.tag == "Branch")
        {
            branches.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AxeLighter")
        {
            pickUpText.SetActive(false);
            inRangeOfAxeAndLighter = false;
        }
        else if (other.gameObject.tag == "Branch")
        {
            branches.Remove(other.gameObject);
        }
    }
}
