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
    public Text bodyHeatText;

    public Gradient gradient;

    public List<BranchHeat> branchHeats;


    // Start is called before the first frame update
    void Start()
    {
        branchHeats = new List<BranchHeat>();
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay = lightingManager.TimeOfDay;

        if (timeOfDay < 5.4f || timeOfDay > 21f)
            isNight = true;
        else
            isNight = false;

        //now calc final body heat change
        //FOR day-nightchange
        float bodyHeatChange = 0;
        if (isNight == true)
            bodyHeatChange -= bodyHeatLossNight;
        else
            bodyHeatChange -= bodyHeatLossDay;

        //now factor in branch heat
        foreach (BranchHeat bH in branchHeats)
        {
            bodyHeatChange += bH.heatGiven;
        }


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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HeatCollider")
        {
            branchHeats.Add(other.gameObject.GetComponent<BranchHeat>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HeatCollider")
        {
            branchHeats.Remove(other.gameObject.GetComponent<BranchHeat>());
        }
    }
}
