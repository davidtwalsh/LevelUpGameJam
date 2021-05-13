using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHeat : MonoBehaviour
{
    public LightingManager lightingManager;
    float timeOfDay;
    float bodyHeat = 100f;
    bool isNight;
    public float bodyHeatLossNight;
    public float bodyHeatLossDay;
    public float waterHeatLoss;
    public Text bodyHeatText;

    public Gradient gradient;

    public List<BranchHeat> branchHeats;

    bool isTouchingWater = false;

    //for fire sound
    public AudioSource fireSound;

    //for displaying temp warning
    public GameObject tempWarning;

    //night warning
    public GameObject nightWarning;
    bool hasShownNightWarning = false;

    public GameObject boat;
    bool boatActivated = false;
    float winTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        branchHeats = new List<BranchHeat>();
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay = lightingManager.TimeOfDay;

        if (timeOfDay < 5.4f || timeOfDay > 18f)
            isNight = true;
        else
            isNight = false;

        if (timeOfDay > 6 && timeOfDay < 6.5 && boatActivated == false)
        {
            boat.SetActive(true);
            boatActivated = true;
        }
        if (boatActivated == true)
        {
            winTimer += Time.deltaTime;
            if (winTimer >= 5)
            {
                SceneManager.LoadScene(3);
            }

        }

        //now calc final body heat change
        //FOR day-nightchange
        float bodyHeatChange = 0;
        if (isNight == true)
            bodyHeatChange -= bodyHeatLossNight;
        else
            bodyHeatChange -= bodyHeatLossDay;

        float bHGiven = 0;
        //now factor in branch heat
        foreach (BranchHeat bH in branchHeats)
        {
            bHGiven += bH.heatGiven;
            bodyHeatChange += bH.heatGiven;
        }

        //for fire audio
        if (bHGiven > 0)
        {
            if (fireSound.isPlaying == false)
                fireSound.Play();
        }
        else
        {
            if (fireSound.isPlaying == true)
                fireSound.Stop();
        }


        //water body heatCange
        if (isTouchingWater == true)
            bodyHeatChange -= waterHeatLoss;


        //finally change body heat by body heat change
        bodyHeat += bodyHeatChange * Time.deltaTime;
        if (bodyHeat >= 100)
            bodyHeat = 100;

        bodyHeatText.text = "";
        bodyHeatText.text += (int)bodyHeat;
        bodyHeatText.color = gradient.Evaluate(bodyHeat / 100);

        //end game if player dies
        if (bodyHeat <= 0)
        {
            SceneManager.LoadScene(2);
        }

        //display temp warning
        if (isTouchingWater == true || bodyHeat <= 30)
        {
            tempWarning.SetActive(true);
        }
        else
            tempWarning.SetActive(false);

        //nightWarning
        if (hasShownNightWarning == false && isNight == true)
        {
            nightWarning.SetActive(true);
            hasShownNightWarning = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HeatCollider")
        {
            branchHeats.Add(other.gameObject.GetComponent<BranchHeat>());
        }

        if (other.gameObject.tag == "water")
        {
            isTouchingWater = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HeatCollider")
        {
            branchHeats.Remove(other.gameObject.GetComponent<BranchHeat>());
        }
        if (other.gameObject.tag == "water")
            isTouchingWater = false;
    }


}
