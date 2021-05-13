using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColdAlert : MonoBehaviour
{
    public float timer = 0;
    public float multiplier;
    public bool isGoingUp = true;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoingUp)
            timer += Time.deltaTime * multiplier;
        else
            timer -= Time.deltaTime * multiplier;

        if (timer >= .7)
        {
            isGoingUp = false;
        }
        else if (timer <= 0)
        {
            isGoingUp = true;
        }

        Color tempC = image.color;
        tempC.a = timer;
        image.color = tempC;
    }
}
