using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    // Start is called before the first frame update
    void Start()
    {
        
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


        //finally change body heat by body heat change
        bodyHeat += bodyHeatChange * Time.deltaTime;
        bodyHeatText.text = "Body Heat: ";
        bodyHeatText.text += (int)bodyHeat + "/100";

    }
}
