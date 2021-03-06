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

    public GameObject lighter;
    public Animator lightAnimator;
    bool hasAxeAndLighter = false;

    //For throwing sticks
    public GameObject branch;

    //FOR throw stick dialogue
    public GameObject throwStickHelp;
    bool hasPickedUpStick = false;


    // Start is called before the first frame update
    void Start()
    {
        branches = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAxeAndLighter == true)
        {
            //switch inventory
            if (Input.GetKeyDown(KeyCode.Alpha1)) //player switching to axe
            {
                axe.SetActive(true);
                lighter.SetActive(false);
                currentInventorySlot = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // player switching to lighter
            {
                axe.SetActive(false);
                lighter.SetActive(true);
                currentInventorySlot = 2;
            }
        }

        //pck up ax and lighter
        if (inRangeOfAxeAndLighter == true && Input.GetKeyDown(KeyCode.E))
        {
            inRangeOfAxeAndLighter = false;
            currentInventorySlot = 1;
            axe.SetActive(true);
            axeLighterPickup.SetActive(false);
            pickUpText.SetActive(false);
            inventoryUI.SetActive(true);
            hasAxeAndLighter = true;
        }

        //trigger axe swing animation
        if (currentInventorySlot == 1 && Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Axe Swing");
            axeAnimator.SetTrigger("playerChops");
        }

        //trigger lighter light
        if (currentInventorySlot == 2 && Input.GetMouseButton(0))
        {
            lightAnimator.SetBool("isHoldingMouseDown", true);
        }
        else if (currentInventorySlot == 2)
        {
            lightAnimator.SetBool("isHoldingMouseDown", false);
        }

        //remove branches that are lit
        if (branches.Count > 0)
        {
            for (int i = 0; i < branches.Count; i++)
            {
                BranchBehavior bb = branches[i].transform.parent.gameObject.GetComponent<BranchBehavior>();
                if (bb.isLit == true)
                    branches.Remove(branches[i]);
            }
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
                
                if (hasPickedUpStick == false)
                {
                    hasPickedUpStick = true;
                    throwStickHelp.SetActive(true);
                }
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
            BranchBehavior bb = other.gameObject.transform.parent.gameObject.GetComponent<BranchBehavior>();
            if (bb.isLit == false)
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
            BranchBehavior bb = other.gameObject.transform.parent.gameObject.GetComponent<BranchBehavior>();
            if (bb.isLit == false)
                branches.Remove(other.gameObject);
        }
    }
}
