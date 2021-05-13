using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QHelpLife : MonoBehaviour
{
    public float lifetime;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime || Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.SetActive(false);
        }
    }
}
